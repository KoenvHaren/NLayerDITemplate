using NLayerDITemplate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLayerDITemplate.Data.Abstract
{
    public interface IValueRepository
    {
        Value GetValue(int id);        
        IQueryable<Value> GetValues();
        void CreateValue(Value value);
        void UpdateValue(Value value);
        void DeleteValue(int id);
    }
}
