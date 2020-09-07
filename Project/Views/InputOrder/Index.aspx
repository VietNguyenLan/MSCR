
@{
    ViewBag.Title = "Index";
    Layout = "~/Views/Shared/LayoutUser.cshtml";
}
<head>

</head>
<body>
    <section class="jumbotron text-center">
        <div class="container">
            <h1 style="margin-top:100px;font-weight:800;">Xác nhận đơn hàng</h1>
        </div>
    </section>

    <div class="container bootstrap snippet">
        <div class="row">
            <div style="display:block;width:600px;margin-left:270px;;margin-right:270px;">
                <div style="text-align:center;">
                    <label style="font-weight:600;font-size:25px;">Mã đơn hàng</label>
                    <input type="text" class="form-control" style="width:100%;" />
                </div>
                <div style="text-align:center; margin-top:30px;">
                    <label style="font-weight:600;font-size:25px;">Mã xác nhận</label>
                    <input type="text" class="form-control" style="width:100%;" />
                </div>
                <button style="width:150px;margin-left:225px;margin-right:225px;margin-top:30px;" id="myBtn" class="btn btn-lg btn-block btn-success">Kiểm tra</button>
            </div>
        </div>
    </div><br /><br />
</body>


