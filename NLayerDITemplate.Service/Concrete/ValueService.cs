using NLayerDITemplate.Data.Abstract;
using NLayerDITemplate.Domain.Models;
using NLayerDITemplate.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLayerDITemplate.Service.Concrete
{
    public class ValueService : IValueService
    {
        private readonly IValueRepository repo;
        //Using constructor injection to inject the repository
        public ValueService(IValueRepository repo)
        {
            this.repo = repo;
        }
        public Value GetValue(int id) { return repo.GetValue(id); }
        public IQueryable<Value> GetValues() { return repo.GetValues(); }
        public void CreateValue(Value value) { repo.CreateValue(value); }
        public void UpdateValue(Value value) { repo.UpdateValue(value); }
        public void DeleteValue(int id) { repo.DeleteValue(id); }
    }
}
