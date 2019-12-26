//hàm được gọi khi click search
function Search() {
    var fullName = document.getElementById("fullNameSearch").value;
    var groupName = document.getElementById("groupIDSearch").value;
    window.location.href = 'ADM002?fullName=' + fullName + '&groupId=' + groupName;
};
//xử lí việc sort 
function Sort(sort, sortField) {
    var fullName = document.getElementById("fullNameSearch").value;
    var groupName = document.getElementById("groupIDSearch").value;
    window.location.href = 'ADM002?fullName=' + fullName + '&groupId=' + groupName + '&sortKey=' + sort + '&sortField=' + sortField+'&'+'type=sort';
}
//xử lí việc paging
function Paging(pageNumber) {
    var fullName = document.getElementById("fullNameSearch").value;
    var groupName = document.getElementById("groupIDSearch").value;
    window.location.href = 'ADM002?fullName=' + fullName + '&groupId=' + groupName + '&currentPage=' + pageNumber + '&type=paging';
}
//xử lí ấn sang trang
function NextOrBack(pageNumber, type) {
    var fullName = document.getElementById("fullNameSearch").value;
    var groupName = document.getElementById("groupIDSearch").value;
    window.location.href = 'ADM002?fullName=' + fullName + '&groupId=' + groupName + '&currentPage=' + pageNumber + '&type=paging' + '&nextOrBack=' + type;
}
// xử lí hiển thị hoặc ẩn
function DisplayOrHide() {
    var divLevel = document.getElementById("divLevelJP");
    if (divLevel.style.display === "none") {
        divLevel.style.display = "block";
    } else {
        divLevel.style.display = "none";
    }
}
// chuyển sang url ADM005
function ShowUser(id) {
    window.location.href = '../Delete/Delete?ID='+id;
}
// chuyển sang url ADM003
function AddUser() {
    window.location.href = '../Insert/InsertUser';
}
// submit form đi insert
function ConfirmInsert() {
    console.log(document.getElementById("userform"));
    document.getElementById("userForm").submit();
}
//submit form đi update
function Edit() {
    document.getElementById("userForm1").submit();
}
// chuyển sang url ADM003
function EditUser(id) {
    window.location.href = '../Insert/InsertUser?id='+id;
}
// back về ADM002
function BackADM002() {
    window.location.href = '../ListUser/ADM002';
}
// xác nhận xóa
function ConfirmDelete() {
    var check = confirm("削除しますが、よろしいでしょうか。");
    if (check) {
        document.getElementById("formDelete").submit();
    }
}
