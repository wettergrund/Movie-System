# Movie database

Welcome to my movie system! This project uses C#, minimal API, and MS SQL server to keep track of movies and genres that a user likes. It also suggests movies to watch based on the user's preferences. Data is fetched from TMDb's movie database to provide accurate and up-to-date information. 



## ⚙ Tech stack 
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white) 
![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![Minimal](https://img.shields.io/badge/Minimal_API-005571?style=for-the-badge&logo=.net)
## Key features

- Connect user to a movie
- Based on connected movies suggest new movies with the same genres.

---
## API calls

### All users

`/API/users/all`

*Return users in the database.*

### Search movies by title

`/API/movies/search/{movieTitle}`

Return a list ov movies using TMDb API.

### Connect user to movie

`/API/movie/userlink` 

Parameters:
- userId: Users DB Primary key.
- extID: movie ID from TMDb
- (optional) rating: Rating between 1 and 5

Links a user to a movie and allows them to rate the movie. 

If the movie does not exist in the database, it will be added from TMDb. The next time someone else is connected to the same movie, it will point to the same database item.

### List movies a user is connected to

`/API/movies/{userId}`

List all movies that user have added to their list, including movie title and users rating.

### Suggest movies based on genres

`/API/movies/suggestion/{userId}`

Based on the movies user is connected to, it will check the movies genres and suggest movies based on that. 

###
`/API/genres/{userId}` 

Will list the genre names that user is connected to, based on the connected movies.

## Database structure

![image](https://user-images.githubusercontent.com/50584818/233791645-335a1d90-4409-4594-86e6-53d4aac12f95.png)

## Configuration

An API key from TMDb is required to utilize their service.

Key should be stored in appsetting.json:

![image](https://user-images.githubusercontent.com/50584818/233992272-ac05cf81-b90c-4366-8043-a15b84e17781.png)




## Sources

The movie details used in this project was obtained from TMDb.


<a href="https://www.themoviedb.org/" target="_blank"><img src="https://www.themoviedb.org/assets/2/v4/logos/v2/blue_square_1-5bdc75aaebeb75dc7ae79426ddd9be3b2be1e342510f8202baf6bffa71d7f5c4.svg" alt=“TMDb_logo” width="200" target="_blank" ></a>
