/*
* This code is based on that of found at http://www.devcurry.com/2013/01aspnet-mvc-cascading-dropdown-list.html
* On 5/5/2016                                       
*/
$(document).ready(function () {
    $("#BuildingId").change(function () {
        $("#StaffId").empty();
        $.ajax({
            type: 'POST',
            url: '@Url.Action("GetAllStaffInBuilding")',
            dataType: 'json',
            data: { id: $("#BuildingId").val() },
            success: function (staffs) {
                $.each(staffs, function (i, staff) {
                    $("#StaffId").append('<option value ="'
                        + staff.StaffId + '">'
                        + staff.Name + '</option');
                });
            },
            error: function (ex) {
                alert('Failed to complete!' + ex);
            }
        });
        return false;
    })
});