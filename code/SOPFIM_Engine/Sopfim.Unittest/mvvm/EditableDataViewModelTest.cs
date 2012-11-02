using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using SOPFIM.DataLayer;
using SOPFIM.Domain;
using Sopfim.ViewModels;

namespace Sopfim.Unittest.mvvm
{
    [TestFixture]
    public class EditableDataViewModelTest
    {
        private TestClass _testClass;
        private Mock<IRepository<Message>>  _repo;
        private List<Message> _messages;
        [SetUp]
        public void Setup()
         {
             _testClass = new TestClass();
            _repo = new Mock<IRepository<Message>>();
            _testClass.Repository = _repo.Object;
            _messages = new List<Message>() {new Message {Application = "App", LarvesBr = 23}, new Message{Application = "App2"}};
            _repo.Setup(x => x.QueryData("where")).Returns(_messages);
         }

        [Test]
        public void When_initialized_IsReadOnly_is_true()
        {
            Assert.IsTrue(_testClass.IsReadOnly);
        }

        [Test]
        public void When_IsReadOnly_true_can_execute_BeginEdit()
        {
            // IsReadOnly is true when initialize
            Assert.IsTrue(_testClass.BeginEdit.CanExecute());
        }

        [Test]
        public void When_IsReadOnly_true_cannot_execute_Save_and_Cancel()
        {
            // IsReadOnly is true when initialize
            Assert.IsFalse(_testClass.CancelEdit.CanExecute());
            Assert.IsFalse(_testClass.SaveEdit.CanExecute());
        }

        [Test]
        public void BeginEdit_set_IsReadOnly_to_False()
        {
            _testClass.BeginEdit.Execute();
            Assert.IsFalse(_testClass.IsReadOnly);
        }

        [Test]
        public void Set_IsReadOnly_to_false_change_can_execute_for_all_edit_commands()
        {
            var commands = new List<string>();
            _testClass.BeginEdit.CanExecuteChanged += (s, e) => commands.Add("Begin");
            _testClass.SaveEdit.CanExecuteChanged += (s, e) => commands.Add("Save");
            _testClass.CancelEdit.CanExecuteChanged += (s, e) => commands.Add("Cancel");
            _testClass.IsReadOnly = false;
            Assert.AreEqual(3, commands.Count);
            Assert.Contains("Begin", commands);
            Assert.Contains("Cancel", commands);
            Assert.Contains("Save", commands);
        }

        [Test]
        public void When_IsReadOnly_false_cannot_execute_BeginEdit()
        {
            _testClass.IsReadOnly = false;
            Assert.IsFalse(_testClass.BeginEdit.CanExecute());
        }

        [Test] 
        public void When_IsReadOnly_false_can_execute_Save_and_Cancel()
        {
            _testClass.IsReadOnly = false;
            Assert.IsTrue(_testClass.CancelEdit.CanExecute());
            Assert.IsTrue(_testClass.SaveEdit.CanExecute());
        }

        [Test]
        public void QueryData_should_delegate_to_repository()
        {
            _testClass.QueryData();
            _repo.Verify(x => x.QueryData("where"));
            Assert.AreEqual(2, _testClass.DataList.Count);
        }

        [Test]
        public void SaveCommand_should_delegate_to_repository()
        {
            _testClass.QueryData();
            _repo.Verify(x => x.QueryData("where"));
            _testClass.DataList[1].IsDirty = true;
            _testClass.SaveEdit.Execute();
            _repo.Verify(x => x.Save(_testClass.DataList.ToList().Where(y => y.IsDirty).ToList()));
        }

        public class TestClass : EditableDataViewModel<Message>
        {
            protected override string WhereTemplate
            {
                get { throw new System.NotImplementedException(); }
            }

            protected override string GenerteWhereClause()
            {
                return "where";
            }

            protected override string TableName
            {
                get { return "MessageTable"; }
            }

            public override void InitialQuery()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}