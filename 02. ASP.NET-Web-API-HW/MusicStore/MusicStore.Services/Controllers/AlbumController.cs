namespace MusicStore.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Data;
    using Models;
    using MusicStore.Models;

    public class AlbumController : ApiController
    {
        private IMusicStoreData Data;

        public AlbumController()
            : this(new MusicStoreData())
        {
        }

        public AlbumController(IMusicStoreData data)
        {
            this.Data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var albums = this.Data.Albums
                .All()
                .Select(AlbumModel.FromAlbum);

            return Ok(albums);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var album = this.Data.Albums
                .All()
                .Select(AlbumModel.FromAlbum)
                .FirstOrDefault(a => a.Id == id);

            if (album == null)
            {
                return BadRequest("There is no album with such id!");
            }

            return Ok(album);
        }

        [HttpPost]
        public IHttpActionResult Create(AlbumModel album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newAlbum = new Album
            {
                Title = album.Title,
                Producer = album.Producer,
                Year = album.Year
            };

            this.Data.Albums.Add(newAlbum);
            this.Data.SaveChanges();

            album.Id = newAlbum.Id;
            return Ok(album);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, AlbumModel album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAlbum = this.Data.Albums
                .All()
                .FirstOrDefault(a => a.Id == id);

            if (existingAlbum == null)
            {
                return BadRequest("There is no album with such id!");
            }

            existingAlbum.Title = album.Title;
            existingAlbum.Producer = album.Producer;
            existingAlbum.Year = album.Year;
            this.Data.SaveChanges();

            album.Id = existingAlbum.Id;
            return Ok(album);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var album = this.Data.Albums
                .All()
                .FirstOrDefault(a => a.Id == id);

            if (album == null)
            {
                return BadRequest("There is no album with such id!");
            }

            this.Data.Albums.Delete(album);
            this.Data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddArtist(int albumId, int artistId)
        {
            var album = this.Data.Albums
                .All()
                .FirstOrDefault(a => a.Id == albumId);

            if (album == null)
            {
                return BadRequest("There is no album with such id!");
            }

            var artist = this.Data.Artists
                .All()
                .FirstOrDefault(a => a.Id == artistId);

            if (artist == null)
            {
                return BadRequest("There is no artist with such id!");
            }

            album.Artists.Add(artist);
            this.Data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddSong(int albumId, int songId)
        {
            var album = this.Data.Albums
                .All()
                .FirstOrDefault(a => a.Id == albumId);

            if (album == null)
            {
                return BadRequest("There is no album with such id!");
            }

            var song = this.Data.Songs
                .All()
                .FirstOrDefault(s => s.Id == songId);

            if (song == null)
            {
                return BadRequest("There is no song with such id!");
            }

            album.Songs.Add(song);
            this.Data.SaveChanges();

            return Ok();
        }
    }
}