using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GPTArticleGen.Model
{
    public class WordpressRepository
    {
        async Task<bool> WordpressAddPostAsync(List<string> tags, string postData, string username, string password, string siteUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                // Check if the tag exists using a separate HttpClient instance
                using (HttpClient tagClient = new HttpClient())
                {
                    //List<string> tags = new List<string>() { "Test", "NNowy1", "testowe" };

                    tagClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}")));
                    foreach (string tagName in tags)
                    {
                        postData = await GetTagsAsync(tagName, tagClient, postData, siteUrl);
                    }

                    if (tags.Count > 0)
                        postData = postData.Replace(",[tag]", "");
                    else
                        postData = postData.Replace("[tag]", "");

                    client.BaseAddress = new Uri($"{siteUrl}/wp-json/wp/v2/posts");
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}")));

                    // Set the content type to JSON
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    // Send the POST request to create a new post
                    HttpResponseMessage response = await client.PostAsync("", new StringContent(postData, System.Text.Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Post created successfully.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Failed to create the post. Status Code: " + response.StatusCode);
                        Console.WriteLine(await response.Content.ReadAsStringAsync()); // Print the response content for debugging
                        return false;
                    }
                }
            }
        }

        async Task<string> GetTagsAsync(string tagName, HttpClient tagClient, string postData, string siteUrl)
        {
            HttpResponseMessage tagResponse = await tagClient.GetAsync($"{siteUrl}/wp-json/wp/v2/tags?slug={tagName}");
            string tagResponseContent = await tagResponse.Content.ReadAsStringAsync();

            if (tagResponse.IsSuccessStatusCode)
            {
                JArray tags = JArray.Parse(tagResponseContent);
                if (tags.Count > 0)
                {
                    // Tag exists, get its ID
                    int tagId = (int)tags[0]["id"];
                    postData = postData.Replace("[tag]", $"{tagId},[tag]");
                }
                else
                {
                    // Tag does not exist, create it using the original HttpClient instance
                    JObject newTag = new JObject();
                    newTag["name"] = tagName;
                    newTag["slug"] = tagName;

                    HttpResponseMessage createTagResponse = await tagClient.PostAsync($"{siteUrl}/wp-json/wp/v2/tags", new StringContent(newTag.ToString(), System.Text.Encoding.UTF8, "application/json"));

                    if (createTagResponse.IsSuccessStatusCode)
                    {
                        string createTagResponseContent = await createTagResponse.Content.ReadAsStringAsync();
                        JToken createdTag = JToken.Parse(createTagResponseContent);
                        int tagId = (int)createdTag["id"];
                        postData = postData.Replace("[tag]", $"{tagId},[tag]");
                    }
                    else
                    {
                        Console.WriteLine("Failed to create the tag. Status Code: " + createTagResponse.StatusCode);
                        Console.WriteLine(await createTagResponse.Content.ReadAsStringAsync());
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed to check for the tag. Status Code: " + tagResponse.StatusCode);
                Console.WriteLine(await tagResponse.Content.ReadAsStringAsync());
            }

            return postData;
        }

    }

}
