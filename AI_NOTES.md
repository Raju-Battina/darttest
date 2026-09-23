### 1. Which AI tool(s) did you use?

I used **ChatGPT** and **GitHub Copilot** during the development of the solution.

### 2. Two or three prompts that helped you the most

Some of the prompts that were particularly helpful were:

* **"Include the code for this requirement."**
* **"Generate the code for this functionality."**
* **"Provide a complete implementation with an explanation."**

These prompts helped accelerate development by providing initial code structures and implementation ideas that I could review, modify, and integrate into the solution.

### 3. One concrete example where the AI's suggestion was wrong or not ideal, and how you detected and corrected it

While implementing API versioning for my ASP.NET Core Web API, the AI initially suggested using:

`[HttpGet(Name = "v1/weather")]`

and indicated that this would version the API.

After reviewing and testing the implementation, I identified that the `Name` property only defines a route name and does not actually implement API versioning. I corrected this by implementing proper API versioning with a versioned route, such as:

`/api/v1/weather`

I then tested the API endpoint to verify that the version was correctly included in the URL.

### 4. Parts of the solution where you chose to write code yourself rather than rely on AI, and why

I manually handled the JSON data structure and mapping between the different applications. The AI-generated code did not always correctly identify the JSON format and the required model structure consistently across the **WeatherImportApp, WeatherAPI, and WeatherApp**.

I therefore reviewed the actual JSON response, identified the required fields and data types, and manually updated the models and mapping where necessary. This ensured that the data format remained consistent across all three applications.
