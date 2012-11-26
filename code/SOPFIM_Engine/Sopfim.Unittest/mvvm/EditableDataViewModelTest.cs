using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ESRI.ArcGIS.Geodatabase;
using Moq;
using NUnit.Framework;
using Sopfim.CustomControls;
using SOPFIM.DataLayer;
using Sopfim.ViewModels;

namespace Sopfim.Unittest.mvvm
{
    [TestFixture]
    public class EditableDataViewModelTest
    {
        private TestEditableListClass _testEditableListClass;
        private Mock<IDataService>  _repo;
        private List<MessageViewModel> _messages;
        private Mock<IMapControl> _mapControl;
        private Mock<ITable> _table;
        [SetUp]
        public void Setup()
         {
             _repo = new Mock<IDataService>();
            _mapControl = new Mock<IMapControl>();
            _table = new Mock<ITable>();
            _repo.Setup(x => x.GetTable("MessageTable")).Returns(_table.Object);

            _testEditableListClass = new TestEditableListClass(_repo.Object, _mapControl.Object);

           
            _messages = new List<MessageViewModel>() { 
                new MessageViewModel { Application = "App", LarvesBr = 23 }, 
                new MessageViewModel { Application = "App2" } };
            _repo.Setup(x => x.GeneralQuery<MessageViewModel>(_table.Object, "where")).Returns(_messages);
         }

        [Test]
        public void When_initialized_IsReadOnly_is_true()
        {
            Assert.IsTrue(_testEditableListClass.IsReadOnly);
        }

        [Test]
        public void When_initialized_should_get_table_from_geodatabase()
        {
            _repo.Verify(x => x.GetTable("MessageTable"));
        }

        [Test]
        public void When_IsReadOnly_true_can_execute_BeginEdit()
        {
            // IsReadOnly is true when initialize
            Assert.IsTrue(_testEditableListClass.BeginEdit.CanExecute());
        }

        [Test]
        public void When_IsReadOnly_true_cannot_execute_Save_and_Cancel()
        {
            // IsReadOnly is true when initialize
            Assert.IsFalse(_testEditableListClass.CancelEdit.CanExecute());
            Assert.IsFalse(_testEditableListClass.SaveEdit.CanExecute());
        }

        [Test]
        public void BeginEdit_set_IsReadOnly_to_False()
        {
            _testEditableListClass.BeginEdit.Execute();
            Assert.IsFalse(_testEditableListClass.IsReadOnly);
        }

        [Test]
        public void Set_IsReadOnly_to_false_change_can_execute_for_all_edit_commands()
        {
            var commands = new List<string>();
            _testEditableListClass.BeginEdit.CanExecuteChanged += (s, e) => commands.Add("Begin");
            _testEditableListClass.SaveEdit.CanExecuteChanged += (s, e) => commands.Add("Save");
            _testEditableListClass.CancelEdit.CanExecuteChanged += (s, e) => commands.Add("Cancel");
            _testEditableListClass.IsReadOnly = false;
            Assert.AreEqual(3, commands.Count);
            Assert.Contains("Begin", commands);
            Assert.Contains("Cancel", commands);
            Assert.Contains("Save", commands);
        }

        [Test]
        public void When_IsReadOnly_false_cannot_execute_BeginEdit()
        {
            _testEditableListClass.IsReadOnly = false;
            Assert.IsFalse(_testEditableListClass.BeginEdit.CanExecute());
        }

        [Test] 
        public void When_IsReadOnly_false_can_execute_Save_and_Cancel()
        {
            _testEditableListClass.IsReadOnly = false;
            Assert.IsTrue(_testEditableListClass.CancelEdit.CanExecute());
            Assert.IsTrue(_testEditableListClass.SaveEdit.CanExecute());
        }

        [Test]
        public void QueryData_should_delegate_to_repository()
        {
            _testEditableListClass.QueryCurrentCriteria();
            _repo.Verify(x => x.GeneralQuery<MessageViewModel>(_table.Object, "where"));
            Assert.AreEqual(2, _testEditableListClass.DataList.Count);
        }


        [Test]
        public void QueryData_should_run_the_filter()
        {
            _testEditableListClass.QueryCurrentCriteria();
            _repo.Verify(x => x.GeneralQuery<MessageViewModel>(_table.Object, "where"));
            Assert.AreEqual(1, _testEditableListClass.DisplayList.Count);
        }

        [Test]
        public void SaveCommand_should_delegate_to_repository()
        {
            _testEditableListClass.QueryCurrentCriteria();
            _testEditableListClass.DataList[1].IsDirty = true;
            _testEditableListClass.SaveEdit.Execute();
            _repo.Verify(x => x.Save(It.Is<List<MessageViewModel>>(y => y.Count == 1), _table.Object));
        }

        [Test]
        public void SetSelectedRecordAsDirty_will_set_selected_record_as_dirty()
        {
            _testEditableListClass.DataList = new ObservableCollection<MessageViewModel>(_messages);
            var list = _testEditableListClass.DataList.ToList().Where(x => x.IsDirty);
            Assert.AreEqual(0, list.Count());
            _testEditableListClass.SelectedRecord = _testEditableListClass.DataList[1];
            _testEditableListClass.SetSelectedRecordAsDirty();
            list = _testEditableListClass.DataList.ToList().Where(x => x.IsDirty);
            Assert.AreEqual(1, list.Count());
        }


        public class TestEditableListClass : EditableListViewModel<MessageViewModel>
        {
            public TestEditableListClass(IDataService service, IMapControl mapControl) : base(service, mapControl)
            {
                
            }

            protected override string GenerteWhereClause()
            {
                return "where";
            }

            protected override string TableName
            {
                get { return "MessageTable"; }
            }

            public override void  InitialQuery()
            {
                QueryCurrentCriteria();
            }

            public override Func<MessageViewModel, bool> FilterCriteria
            {
                get
                {
                    return (e) => e.Application == "App2";
                }
            }
        }
    }
}