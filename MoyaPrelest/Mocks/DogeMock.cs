using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using MoyaPrelest.Models;
using System.Text.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoyaPrelest.Mocks
{
    public class DogeMock
    {

        private string DataBasePath = "db.json";
        public List<DogeModel> dogs;
        //public DogeMock()
        //{

        //    dogs =
        //            new List<DogeModel>
        //            {
        //                new DogeModel {Breed = "лох-терьер", Name = "лох" },
        //                new DogeModel {Breed = "овчарка", Name = "буч"}
        //            };
        //}

        public DogeMock() => LoadDataBase();

        public void SaveDataBase()
        {
            using (StreamWriter sw = new StreamWriter(DataBasePath))
            {
                sw.Write(JsonSerializer.Serialize(dogs));
            }
        }
        public void LoadDataBase()
        {
            using (StreamReader sr = new StreamReader(DataBasePath))
            {
                dogs = JsonSerializer.Deserialize<List<DogeModel>>(sr.ReadToEnd());
            }
        }
        public void AddtoDataBase(DogeModel d)
        {
            if (dogs == null)
            {
                dogs = new List<DogeModel>();
            }
            dogs.Add(d);
            SaveDataBase();
        }
        public void DeleteFromDataBase(string Name)
        {
            dogs.Remove(dogs.Find(t => t.Name == Name));
            SaveDataBase();
        }

        public List<string> BreedType()
        {
            return (from t in dogs select t.Breed).Distinct().ToList();
        }
        public List<DogeModel> BreedGrouping(string b)
        {
            return (from t in dogs where t.Breed == b select t).ToList();
        }

        public double BreedAverageAge(string b)
        {
            return (from t in dogs where t.Breed == b select t.Age).Average();
        }

        public List<DogeModel> AgeRanking(string b)
        {
            return (from t in dogs where t.Breed == b orderby t.Age descending select t).ToList();
        }

        public Dictionary<int, List<DogeModel>> AgeGroup()
        {
            Dictionary<int, List<DogeModel>> res = new Dictionary<int, List<DogeModel>>();

            foreach (DogeModel dog in dogs)
            {
                if (!res.ContainsKey(dog.Age))
                {
                    res.Add(dog.Age, new List<DogeModel> { dog });
                }
                else
                {
                    res[dog.Age].Add(dog);
                }
            }

            return res;
        }

        public Dictionary<int, int> AgeGroupQuantity(string inputBreed)
        {
            Dictionary<int, int> res = new Dictionary<int, int>();
            var group = AgeGroupBreedFiltered(inputBreed);

            foreach(int key in group.Keys)
            {
                res.Add(key, group[key].Count());
            }

            return res;
        }

        public Dictionary<int, List<DogeModel>> AgeGroupBreedFiltered(string inputBreed)
        {
            Dictionary<int, List<DogeModel>> res = new Dictionary<int, List<DogeModel>>();
            var prev = AgeGroup();
            List<DogeModel> buf = new List<DogeModel>();

            foreach (int key in prev.Keys)
            {
                buf = (from t in prev[key] where t.Breed == inputBreed select t).ToList();
                if (buf.Any())
                {
                    res.Add(key, buf);
                }
            }

            return res;
        }
    }
}
