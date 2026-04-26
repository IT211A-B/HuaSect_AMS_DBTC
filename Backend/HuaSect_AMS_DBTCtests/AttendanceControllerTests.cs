using Moq;
using HuaSect_AMS_DBTC.Controllers;
using HuaSect_AMS_DBTC.Service;

namespace HuaSect_AMS_DBTCtests;

[TestClass]
public sealed class AttendanceControllerTests
{
    private Mock<IAttendanceService> _serviceMock;
    private AttendanceController _controller;

    [TestInitialize]
    public void Setup()
    {
        _serviceMock = new Mock<IAttendanceService>();
        _controller = new AttendanceController(_serviceMock.Object);
    }
    [TestMethod]
    public void GetAllAttendanceRecordsTest()
    {
    }
}
