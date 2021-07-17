using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using EncomposApi.Common.Tests;

namespace EncomposApi.Types.Optional.Tests
{
    public class OptionalPerson
    {
        public int Id { get; set; }
        public Optional<string> Name { get; set; }
        public Optional<int> Visits { get; set; }
        public Optional<DateTime> Birthday { get; set; }
        public Optional<DateTimeOffset> BirthdayOffset { get; set; }
        public Optional<DateTime?> LastVisit { get; set; }
        public Optional<decimal> TotalSpend { get; set; }
        public Optional<int?> FavoriteNumber { get; set; }
        public Optional<CardInfo> Card { get; set; }

        public class CardInfo { 
            public string LastFour { get; set; }
            public string Expires { get; set; }
        }
    }

    public class OptionalTests : TestBase
    {
        [Fact]
        public void Optional_ValuesCompareWell()
        {
            Optional<int> valA = 1;
            Optional<int> valB = 2;
            Optional<int> valC = 2;

            Assert.True(valA < valB, "valA should be less than valB");
            Assert.True(valA <= valB, "valA should be less than valB");
            Assert.True(valB > valA, "valB should be greater than valA");
            Assert.True(valB >= valA, "valB should be greater than valA");
            Assert.True(valB >= valC, "valB should equal valC");
            Assert.True(valB <= valC, "valB should equal valC");
        }

        [Fact]
        public void Optional_NullToAbsentValuesMissingWhenSerialized()
        {
            var birthday = DateTime.Now.AddYears(-40);
            var person = new OptionalPerson
            {
                Id = 100,
                Visits = 0,
                Birthday = birthday,
                BirthdayOffset = new DateTimeOffset(birthday),
                LastVisit = null,
                FavoriteNumber = 7,
                Card = new OptionalPerson.CardInfo { Expires = "09/23", LastFour = "5555" }
            };
            person.LastVisit = person.LastVisit.NotNull(); // converts null to absent

            var json = JsonConvert.SerializeObject(person);
            var jobj = JObject.Parse(json);
            Assert.Equal(person.Id, jobj.Value<int>("id"));
            Assert.Equal(person.Visits, jobj.Value<int>("visits"));
            Assert.Equal(person.FavoriteNumber, jobj.Value<int>("favoriteNumber"));
            Assert.Equal(person.Birthday, jobj.Value<DateTime>("birthday"));
            Assert.Equal(person.BirthdayOffset, new DateTimeOffset(jobj.Value<DateTime>("birthdayOffset")));
            Assert.False(jobj.ContainsKey("lastVisit"));
            Assert.Equal(6, jobj.Count);
        }

        [Fact]
        public void Optional_NullValuesPresetWhenDeserialized()
        {
            var birthday = DateTime.Now.AddYears(-40);
            var birthdayOffset = new DateTimeOffset(birthday);
            var jobj = new JObject
            {
                ["id"] = 100,
                ["visits"] = 0,
                ["lastVisit"] = null,
                ["birthday"] = birthday,
                ["birthdayOffset"] = birthdayOffset,
                ["favoriteNumber"] = 7,
                ["card"] = new JObject
                { 
                    ["lastFour"] = "5555",
                    ["expires"] = "09/23"
                }
            };
            var person = jobj.ToObject<OptionalPerson>();
            Assert.Equal(100, person.Id);
            Assert.Equal(0, person.Visits);
            Assert.Equal(7, person.FavoriteNumber);
            Assert.Equal(birthday, person.Birthday);
            Assert.Equal(birthdayOffset, person.BirthdayOffset);
            Assert.True(person.LastVisit.Contains(null));
            Assert.False(person.Name.IsPresent);
            Assert.Equal("5555", person.Card.Select(i => i.LastFour).FirstOrDefault());
            Assert.Equal("09/23", person.Card.Select(i => i.Expires).FirstOrDefault());
        }
    }
}
