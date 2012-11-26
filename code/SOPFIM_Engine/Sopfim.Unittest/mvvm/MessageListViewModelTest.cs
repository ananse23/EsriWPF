using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using Moq;
using NUnit.Framework;
using Sopfim.CustomControls;
using SOPFIM.DataLayer;
using Sopfim.ViewModels;
using SopfimMessage.ViewModel;

namespace Sopfim.Unittest.mvvm
{
    [TestFixture]
    public class MessageListViewModelTest
    {
        private MessageListViewModel _viewModel;
        private Mock<IDataService> _dataService;
        private Mock<IMapControl> _mapControl;
        private List<MessageViewModel> _messages;

        [SetUp]
        public void Setup()
        {
            _dataService = new Mock<IDataService>();
            _mapControl = new Mock<IMapControl>();
            _messages = new List<MessageViewModel>()
                            {
                                new MessageViewModel() {MessagesID = 2, NoBloc = "b2"},
                                new MessageViewModel() {MessagesID = 12, NoBloc = "b12"}
                            };
            _viewModel = new MessageListViewModel(_dataService.Object, _mapControl.Object);
            _dataService.Setup(x => x.GeneralQuery<MessageViewModel>(It.IsAny<ITable>(), null)).Returns(_messages);
        }

        [Test]
        public void Set_IsReadOnly_to_false_change_can_execute_for_all_edit_commands()
        {
            var commands = new List<string>();
            _viewModel.OpenLastMessage.CanExecuteChanged += (s, e) => commands.Add("LastMessage");
            _viewModel.NewMessage.CanExecuteChanged += (s, e) => commands.Add("NewMessage");
            _viewModel.IsReadOnly = false;
            Assert.AreEqual(2, commands.Count);
            Assert.Contains("LastMessage", commands);
            Assert.Contains("NewMessage", commands);
        }

        [Test]
        public void Set_Message_Number_should_requery_database()
        {
            _viewModel.MessageNumber = 1;
            //_dataService.Verify(x => x.GeneralQuery<MessageViewModel>());
        }

        [Test]
        public void InitialQuery_should_Get_all_records()
        {
            _dataService.Setup(x => x.GeneralQuery<MessageViewModel>(It.IsAny<ITable>(), "MessagesID = 12")).Returns(
                new List<MessageViewModel> {_messages[1]});
            _viewModel.InitialQuery();
            Assert.AreEqual(12, _viewModel.MessageNumber);
        }

        [Test]
        public void set_Message_number_should_query_db()
        {
            _dataService.Setup(x => x.GeneralQuery<MessageViewModel>(It.IsAny<ITable>(), "MessagesID = 16")).Returns(
                new List<MessageViewModel>());
            _viewModel.MessageNumber = 16;
            _dataService.Verify(x => x.GeneralQuery<MessageViewModel>(It.IsAny<ITable>(), "MessagesID = 16"));
        }

    }
}