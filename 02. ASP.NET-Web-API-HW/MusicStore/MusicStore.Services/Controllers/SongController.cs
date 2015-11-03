namespace MusicStore.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Data;
    using Models;
    using MusicStore.Models;

    public class SongController : ApiController
    {
        private IMusicStoreData Data;

        public SongController()
            : this(new MusicStoreData())
        {
        }

        public SongController(IMusicStoreData data)
        {
            this.Data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var songs = this.Data
                .Songs
                .All()
                .Select(SongModel.FromSong)
                .ToList();

            return Ok(songs);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var song = this.Data
                .Songs
                .All()
                .Where(s => s.Id == id)
                .Select(SongModel.FromSong)
                .FirstOrDefault();

            if (song == null)
            {
                return BadRequest("There is no song with such id!");
            }

            return Ok(song);
        }

        [HttpPost]
        public IHttpActionResult Create(SongModel song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newSong = new Song()
            {
                Title = song.Title,
                Genre = song.Genre,
                Year = song.Year
            };

            this.Data.Songs.Add(newSong);
            this.Data.SaveChanges();

            song.Id = newSong.Id;
            return Ok(song);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, SongModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var song = this.Data
                .Songs
                .All()
                .Where(s => s.Id == id)
                .Select(SongModel.FromSong)
                .FirstOrDefault();

            if (song == null)
            {
                return BadRequest("There is no song with such id!");
            }

            song.Title = model.Title;
            song.Genre = model.Genre;
            song.Year = model.Year;
            this.Data.SaveChanges();

            model.Id = id;
            return Ok(model);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var song = this.Data
                .Songs
                .All()
                .FirstOrDefault(s => s.Id == id);

            if (song == null)
            {
                return BadRequest("There is no song with such id!");
            }

            this.Data.Songs.Delete(song);
            this.Data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddArtist(int artistId, int songId)
        {
            var artist = this.Data
                .Artists
                .All()
                .FirstOrDefault(a => a.Id == artistId);

            if (artist == null)
            {
                return BadRequest("There is no artist with such id!");
            }

            var song = this.Data
                .Songs
                .All()
                .FirstOrDefault(s => s.Id == songId);

            if (song == null)
            {
                return BadRequest("There is no song with such id!");
            }

            song.Artist = artist;
            this.Data.SaveChanges();

            return Ok();
        }
    }
}