using NLayerDITemplate.Data.Abstract;
using NLayerDITemplate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLayerDITemplate.Data.Concrete
{
    public class ValueRepository : IValueRepository
    {              
        public Value GetValue(int id) { throw new NotImplementedException(); }
        public IQueryable<Value> GetValues() { throw new NotImplementedException(); }
        public void CreateValue(Value value) { }
        public void UpdateValue(Value value) { }
        public void DeleteValue(int id) { }
    }
}
