﻿using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace GPTArticleGen.Model
{
    public class WordpressRepository
    {
        public async Task<bool> AddPostAsync(List<string> tags, ArticleModel article)
        {
            using (HttpClient client = new HttpClient())
            {
                // Check if the tag exists using a separate HttpClient instance
                using (HttpClient tagClient = new HttpClient())
                {
                    //List<string> tags = new List<string>() { "Test", "NNowy1", "testowe" };

                    tagClient.DefaultRequestHeaders.Add("Authorization", "Basic " 
                        + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{article.Username}:{article.Password}")));
                    foreach (string tagName in tags)
                    {
                        article.PostData = await GetTagsAsync(tagName, tagClient, article.PostData, article.Site);
                    }

                    if (tags.Count > 0)
                        article.PostData = article.PostData.Replace(",[tag]", "");
                    else
                        article.PostData = article.PostData.Replace("[tag]", "");

                    // Check if the featured image is set
                    if (!string.IsNullOrEmpty(article.ImagePath))
                    {
                        // Upload and set the featured image
                        string featuredImageId = await UploadFeaturedImageAsync(article.ImagePath, article.Site, article.Username, article.Password, article.Id);
                        if (!string.IsNullOrEmpty(featuredImageId))
                        {
                            article.PostData = article.PostData.Replace("[featured_image]", featuredImageId);
                        }
                    }

                    client.BaseAddress = new Uri($"{article.Site}/wp-json/wp/v2/posts");
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " 
                        + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{article.Username}:{article.Password}")));

                    // Set the content type to JSON
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    // Send the POST request to create a new post
                    HttpResponseMessage response = await client.PostAsync("", new StringContent(article.PostData, System.Text.Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Post created successfully.");
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var postResponse = JsonConvert.DeserializeObject<PostCreationResponse>(responseContent);

                        if (postResponse != null)
                        {
                            // Use the extracted post ID to construct the URL of the newly created post
                            Debug.WriteLine("Post ID: " + postResponse.Id);
                            article.Id = postResponse.Id;
                            article.PostUrl = postResponse.Link;
                            article.Date = DateTime.Now;

                            return true;
                        }
                        else
                        {
                            Debug.WriteLine("Failed to extract post ID from the response content.");
                            return false;
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Failed to create the post. Status Code: " + response.StatusCode);
                        Debug.WriteLine(await response.Content.ReadAsStringAsync()); // Print the response content for debugging
                        return false;
                    }
                }
            }
        }

        async Task<string> GetTagsAsync(string tagName, HttpClient tagClient, string postData, string siteUrl)
        {
            // Remove any special characters from the tag name
            // Comment if you want to allow special characters
            tagName = tagName.Replace(".", "");
            tagName = tagName.Replace(",", "");
            tagName = tagName.Replace(":", "");
            tagName = tagName.Replace(";", "");

            // Generate the slug from the tag name
            string tagSlug = Slugify(tagName);

            HttpResponseMessage tagResponse = await tagClient.GetAsync($"{siteUrl}/wp-json/wp/v2/tags?slug={tagSlug}");
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
                    newTag["slug"] = tagSlug;

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

        async Task<string> UploadFeaturedImageAsync(string imagePath, string siteUrl, string username, string password, int articleId)
        {
            using (HttpClient client = new HttpClient())
            {
                // Check if the file exists
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine("Image file does not exist.");
                    return null;
                }

                // Open the image file as a stream
                using (var imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    // Set up the request to upload the image
                    client.BaseAddress = new Uri($"{siteUrl}/wp-json/wp/v2/media");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));

                    var content = new MultipartFormDataContent();
                    content.Add(new StreamContent(imageStream), "file", "featured-image.png");

                    HttpResponseMessage imageResponse = await client.PostAsync("", content);
                    if (imageResponse.IsSuccessStatusCode)
                    {
                        // Extract the newly uploaded image's ID from the response
                        string imageResponseContent = await imageResponse.Content.ReadAsStringAsync();
                        JObject image = JObject.Parse(imageResponseContent);
                        SQLiteDB db = new SQLiteDB();
                        string imageId = image["id"].ToString();
                        return imageId;
                    }
                    else
                    {
                        Console.WriteLine("Failed to upload featured image. Status Code: " + imageResponse.StatusCode);
                        Console.WriteLine(await imageResponse.Content.ReadAsStringAsync());
                        return null;
                    }
                }
            }
        }

        string Slugify(string input)
        {
            // Implement a slugification logic to convert the tag name to a slug
            // For example, you can replace spaces with dashes and convert to lowercase
            return input.Replace(" ", "-").ToLower();
        }
    }

    public class PostCreationResponse
    {
        public int Id { get; set; }
        public string Link { get; set; }
    }
}
