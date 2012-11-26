using System;
using SOPFIM.Domain;
using Sopfim.ViewModels;

namespace Sopfim.Unittest
{
    public class TestListViewModel : EditableListViewModel<TestEntityViewModel>
    {
        public int Couter { get; set; }

        public TestListViewModel()
        {
            Couter = 0;
        }

        protected override string GenerteWhereClause()
        {
            throw new NotImplementedException();
        }

        protected override string TableName
        {
            get { return string.Empty; }
        }

        public override void InitialQuery()
        {
            Couter = 2;
        }

        public override Func<TestEntityViewModel, bool> FilterCriteria
        {
            get { throw new NotImplementedException(); }
        }
    }


    public class TestEntityViewModel : EditableEntity
    {
        
    }
}