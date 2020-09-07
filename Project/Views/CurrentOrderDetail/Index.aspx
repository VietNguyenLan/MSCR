﻿
@{
    ViewBag.Title = "InProcess";
    Layout = "~/Views/Shared/LayoutUser.cshtml";
}
@model IEnumerable<Project.EF.order_detail>
<head>
    <link href="~/fonts/Font(css,js)/OrderList.css" rel="stylesheet" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link href="~/fonts/Font(css,js)/COModal.css" rel="stylesheet" />
</head>
<body>
    <section class="jumbotron text-center">
        <div class="container">
            <h1 style="margin-top:80px;font-weight:700;">Chi tiết đơn hàng</h1>
        </div>
    </section>


    <div class="container mb-4">
        <div class="row">
            <div style="display:block; width:600px;margin-left:270px;margin-right:270px;">
                <div style="display:flex; width:400px;margin-left:100px;margin-right:100px;margin-bottom:20px;">
                    <div style="margin-right: 90px"><span style="font-weight:500;font-size:18px;">Mã đơn hàng: </span>@ViewBag.orderID</div>
                    <div><span style="font-weight:500;font-size:18px;">Mã nhận đơn: </span>@ViewBag.code</div>
                </div>
                <div style="display:flex; width:400px;margin-left:100px;margin-right:100px;margin-bottom:20px;">
                    <div style="margin-right: 55px"><span style="font-weight:500;font-size:18px;">Ngày sử dụng: </span>@ViewBag.takeDate</div>
                    <div><span style="font-weight:500;font-size:18px;">Bữa ăn: </span>@ViewBag.takeTime</div>
                </div>
            </div>
            <div style="border: 1px dotted red;border-radius: 5px;width:600px;margin-left:270px;margin-right:270px;text-align:center;">
                <div class="col-12">
                    <br />
                    <div class="table-responsive">
                        <table class="table table-borderless">

                            <thead>
                                <tr>
                                    <th>
                                        Tên món ăn
                                    </th>
                                    <th>
                                        Số tiền
                                    </th>
                                    <th>
                                        Số lượng
                                    </th>
                                </tr>
                            </thead>

                            <tbody>

                                @foreach (var item in Model)
                                {
                                    <tr>

                                        <td>
                                            @Html.DisplayFor(modelItem => item.product.name)
                                        </td>
                                        <td>
                                            @Html.DisplayFor(modelItem => item.price) đ
                                        </td>
                                        <td>
                                            @Html.DisplayFor(modelItem => item.quantity)
                                        </td>
                                    </tr>
                                }
                            </tbody>

                        </table>
                        <hr /><br />
                        <h4 class="alignRight">Tổng số tiền @ViewBag.total đ </h4><br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="text-center">

        <a href="~/OrderList/Index" class="btn btn-success text-white" style="cursor:pointer; width:100px;height:40px;">
            Quay lại
        </a>
    </div>
    <div id="myModal" class="modal">

        <!-- Modal content -->
        <div class="modal-content">
            <div class="modal-header">
                <span class="close">&times;</span>
            </div>
            <div class="modal-body">
                <h4>Bạn có muốn hủy đơn hàng của mình không?</h4>
                <div style="display: flex; align-items: center; justify-content: center;margin-top:30px;margin-bottom:20px;">
                    <a href="#" class="btn btn-success my-0 p" style="margin-right:30px;width:150px;">
                        Xác Nhận
                    </a>
                    <button class="btn btn-danger my-0 p" type="submit" style="width:150px;">
                        Hủy
                    </button>
                </div>
            </div>

        </div>

    </div>
    <script src="~/fonts/Font(css,js)/COModal.js"></script>
</body>


