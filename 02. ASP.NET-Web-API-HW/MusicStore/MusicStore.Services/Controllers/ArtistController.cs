namespace MusicStore.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Data;
    using Models;
    using MusicStore.Models;

    public class ArtistController : ApiController
    {
        private IMusicStoreData Data;

        public ArtistController()
            : this(new MusicStoreData())
        {
        }

        public ArtistController(IMusicStoreData data)
        {
            this.Data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var artists = this.Data
                .Artists
                .All()
                .Select(ArtistModel.FromArtist);

            return Ok(artists);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var artist = this.Data
                .Artists
                .All()
                .Select(ArtistModel.FromArtist)
                .FirstOrDefault(a => a.Id == id);

            if (artist == null)
            {
                return BadRequest("There is no artist with this id!");
            }

            return Ok(artist);
        }

        [HttpPost]
        public IHttpActionResult Create(ArtistModel artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newArtist = new Artist
            {
                Name = artist.Name,
                Country = artist.Country,
                DateOfBirth = artist.DateOfBirth
            };

            this.Data.Artists.Add(newArtist);
            this.Data.SaveChanges();

            artist.Id = newArtist.Id;
            return Ok(artist);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, ArtistModel artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingArtist = this.Data.Artists
                .All()
                .FirstOrDefault(a => a.Id == id);

            if (existingArtist == null)
            {
                return BadRequest("There is no artist with such id!");
            }

            existingArtist.Name = artist.Name;
            existingArtist.Country = artist.Country;
            existingArtist.DateOfBirth = artist.DateOfBirth;
            this.Data.SaveChanges();

            artist.Id = id;
            return Ok(artist);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var artist = this.Data.Artists
                .All()
                .FirstOrDefault(a => a.Id == id);

            if (artist == null)
            {
                return BadRequest("There is no artist with such id!");
            }

            this.Data.Artists.Delete(artist);
            this.Data.SaveChanges();

            return Ok();
        }

    }
}