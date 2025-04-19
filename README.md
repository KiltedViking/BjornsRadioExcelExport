# BjornsRadioExcelExport

Project to test export to spreadsheet using NuGet packages ClosedXML and ClosedXML.Extensions.Mvc plus using `Aggregate()` method in LINQ to create a string out of child entities (songs) to a parent entity (album).

Project is built using an existing database and using Microsoft's Entity Framework to generate model classes and context class, and then customising using partial classes and "meta data classes" to add attributes to properties of model classes, to avoid overwriting any customisations should model be regenerated.

## Data model

### Album

* `Id` - unique identifier for album.
* `Artist` - name of group or artist.
* `Title` - name of album.
* `ReleaseYear` - year that album was released.
* `Genre` - reference to genre of album.
* `Media` - media of album, e.g. CD.
* `Comments` - any comments on album.
 
 ### Song

 * `Id` - unique identifier for song.
 * `Album` - reference to album song in on.
 * `AlbumOrder` - order in which song appears on album.
 * `Title` - name of song.
 * `Comments` - any comments on song.

 ### Genre

 * `Id` - unique identifier for genre.
 * `GenreName` - name of genre, e.g. Pop and Rock.
 * `Comments` - any comments on genre.

 ### MediaType

 * `Id` - unique identifier for media.
 * `TypeName` - name of media, e.g. CD or Vinyl.
 * `Comments` - any comments on media.

## Set up project

Below are steps on how to set up project.

* Clone repository.
* Open solution in Visual Studio (I used Community 2022 edition).
* Right-click file `libman.json` and select Restore Client-Side Libraries (to download bootstrap-icons, etc., or action buttons will look strange ;-)).
* Add connection string `BjornsRadioConnection` for database - recommended is using user secrets, i.e. right-click project and select Manage User Secrets. (Tip: Add data connection to database in VS's Server Explorer pane and then copy connection string from the created connection's properties.)

---

Björn G. D. Persson, kiltedviking.net, 2024-06-05