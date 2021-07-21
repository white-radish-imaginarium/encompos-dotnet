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
        public void Optional_ValuesCompareToBareValues()
        {
            Optional<int> one = 1;
            Optional<int?> nullableOne = 1;
            Optional<int?> nullableNull = null;
            Optional<int?> absent = nullableNull.NotNull();

            Assert.True(one.IsPresent, $"{nameof(one)} should be present");
            Assert.False(one.IsAbsent, $"{nameof(one)} should not be absent");
            Assert.True(one == 1, $"{nameof(one)} should be equal to 1");
            Assert.True(one >= 1, $"{nameof(one)} should be greater than or equal to 1");
            Assert.True(one <= 1, $"{nameof(one)} should be less than or equal to 1");
            Assert.True(one < 2, $"{nameof(one)} should be less than 2");
            Assert.True(one <= 2, $"{nameof(one)} should be less than or equal to 2");
            Assert.False(one > 2, $"{nameof(one)} should not be more than 2");
            Assert.False(one >= 2, $"{nameof(one)} should not be more than or equal to 2");
            Assert.True(one > 0, $"{nameof(one)} should be greater than 0");
            Assert.True(one >= 0, $"{nameof(one)} should be greater than or equal to 0");
            Assert.False(one < 0, $"{nameof(one)} should not be less than 0");
            Assert.False(one <= 0, $"{nameof(one)} should not be less than or equal to 0");

            Assert.True(nullableOne.IsPresent, $"{nameof(nullableOne)} should be present");
            Assert.False(nullableOne.IsAbsent, $"{nameof(nullableOne)} should not be absent");
            
            Assert.True(nullableOne == 1, $"{nameof(nullableOne)} should be equal to 1");
            Assert.False(nullableOne > 1, $"{nameof(nullableOne)} should not be greater than 1");
            Assert.True(nullableOne >= 1, $"{nameof(nullableOne)} should be greater than or equal to 1");
            Assert.False(nullableOne < 1, $"{nameof(nullableOne)} should not be less than 1");
            Assert.True(nullableOne <= 1, $"{nameof(nullableOne)} should be less than or equal to 1");

            Assert.False(nullableOne == 2, $"{nameof(nullableOne)} should not be equal to 2");
            Assert.True(nullableOne != 2, $"{nameof(nullableOne)} should be not equal to 2");
            Assert.False(nullableOne > 2, $"{nameof(nullableOne)} should not be greater than 2");
            Assert.False(nullableOne >= 2, $"{nameof(nullableOne)} should not be greater than or equal to 2");
            Assert.True(nullableOne < 2, $"{nameof(nullableOne)} should be less than 2");
            Assert.True(nullableOne <= 2, $"{nameof(nullableOne)} should be less than or equal to 2");

            Assert.False(nullableOne == 0, $"{nameof(nullableOne)} should not be equal to 0");
            Assert.True(nullableOne != 0, $"{nameof(nullableOne)} should be not equal to 0");
            Assert.True(nullableOne > 0, $"{nameof(nullableOne)} should be greater than 0");
            Assert.True(nullableOne >= 0, $"{nameof(nullableOne)} should be greater than or equal to 0");
            Assert.False(nullableOne < 0, $"{nameof(nullableOne)} should not be less than 0");
            Assert.False(nullableOne <= 0, $"{nameof(nullableOne)} should not be less than or equal to 0");
            
            Assert.False(nullableOne == null, $"{nameof(nullableOne)} should not be equal to null");
            Assert.True(nullableOne != null, $"{nameof(nullableOne)} should be not equal to null");
            Assert.False(nullableOne > null, $"{nameof(nullableOne)} should not be greater than null");
            Assert.False(nullableOne >= null, $"{nameof(nullableOne)} should not be greater than or equal to null");
            Assert.False(nullableOne < null, $"{nameof(nullableOne)} should not be less than null");
            Assert.False(nullableOne <= null, $"{nameof(nullableOne)} should not be less than or equal to null");

            Assert.False(nullableOne == absent, $"{nameof(nullableOne)} should not be equal to absent");
            Assert.True(nullableOne != absent, $"{nameof(nullableOne)} should be not equal to absent");
            Assert.False(nullableOne < absent, $"{nameof(nullableOne)} should not be less than absent");
            Assert.False(nullableOne <= absent, $"{nameof(nullableOne)} should not be less than or equal to absent");
            Assert.False(nullableOne > absent, $"{nameof(nullableOne)} should not be greater than absent");
            Assert.False(nullableOne >= absent, $"{nameof(nullableOne)} should not be greater than or equal to absent");

            Assert.True(nullableNull.IsPresent, $"{nameof(nullableNull)} should be present");
            Assert.False(nullableNull.IsAbsent, $"{nameof(nullableNull)} should not be absent");
            Assert.False(nullableNull == 1, $"{nameof(nullableNull)} should not be equal to 1");
            Assert.False(nullableNull < 2, $"{nameof(nullableNull)} should not be less than 2");
            Assert.False(nullableNull > 0, $"{nameof(nullableNull)} should not be greater than 0");
            Assert.False(nullableNull != null, $"{nameof(nullableNull)} should not be not equal to null");
            Assert.True(nullableNull == null, $"{nameof(nullableNull)} should be equal to null");
            Assert.True(nullableNull != absent, $"{nameof(nullableNull)} should be not equal to absent");
            Assert.False(nullableNull == absent, $"{nameof(nullableNull)} should not be equal to absent");
            Assert.False(nullableNull < absent, $"{nameof(nullableNull)} should not be less than absent");
            Assert.False(nullableNull > absent, $"{nameof(nullableNull)} should not be greater than absent");

            Assert.True(absent.IsAbsent, $"{nameof(absent)} should be absent");
            Assert.False(absent.IsPresent, $"{nameof(absent)} should not be present");
            Assert.False(absent == 1, $"{nameof(absent)} should not be equal 1");
            Assert.False(absent < 2, $"{nameof(absent)} should not be less than 2");
            Assert.False(absent > 0, $"{nameof(absent)} should not be greater than 0");
            Assert.True(absent != null, $"{nameof(absent)} should be not equal to null");
            Assert.False(absent == null, $"{nameof(absent)} should not be equal to null");
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
