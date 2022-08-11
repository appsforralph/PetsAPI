
# ASP.NET Core WebApi DogCat API

This .NET Core API is an exercise for showcasing the ability in developing a REST API and integrating 3rd
party services. The two 3rd party involve are https://thedogapi.com/ and https://thecatapi.com/.
This repository contains a controller which is dealing with the combination of Dog and Cat API. All of the methods used are in GET.

Hope this helps.

See the examples here: 

## Versions

``` https://localhost:44301/swagger ```

![image](https://user-images.githubusercontent.com/30335870/183977703-79b4d8c7-d783-4334-b3ec-7426cadd92f5.png)


## Setting Up

To setup this project, you need to clone the git repo

```sh
$ git clone https://github.com/appsforralph/PetsAPI.git
$ cd PetsAPI
```

followed by

```sh
$ dotnet restore
```

### Prerequisite:

- .NET Core 3.1
- Visual Studio 2019 or greater versions

### Running the Project
Alter first the apiKeys needed. It is located in the appsetting.json
- apiKey : this for the APIKey authorization of PETSAPI. This is needed in every request.
- dogapiKey : the apiKey needed by the api https://thedogapi.com/. You can get the api key on the said site.
- catapiKey : the apiKey needed by the api https://thecatapi.com/. You can get the api key on the said site.


On initial, it is setup to run on  https://localhost:44301/

![image](https://user-images.githubusercontent.com/30335870/184063542-b29a93ad-75d8-453c-bd90-dc2cf340911b.png)

Then we could test this in Postman. I have attached the sample request via Postman Collection. Please check in the repository name 'PETsAPI.postman_collection'

Please take note of the following parameters in making a request
- limit : I have set the parameter 'Limit' to a max of 25 only. Anything that tries above it will default to 25.
- x-api-key : This must be present in the request headers. This serves as the simple Authentication for the API

### Get All Pet details
![image](https://user-images.githubusercontent.com/30335870/184064265-b89ee2c7-c8f1-4597-aedb-30b91a3d8bb2.png)

### Get Pets Images filter by Breed
![image](https://user-images.githubusercontent.com/30335870/184064286-0a62006d-c3e9-4e77-8d77-d4074440c2bf.png)

### Get All Pet Images
![image](https://user-images.githubusercontent.com/30335870/184064319-899d8c9c-9ae3-46b3-8109-b8fd2e048d33.png)

### Get Image filter by image id
![image](https://user-images.githubusercontent.com/30335870/184064347-1cba54a8-a4d4-4bb7-a3ba-c869f2bb51f4.png)

