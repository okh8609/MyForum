
//畫面載入後事件
$(function () {
    //綁定新增文章按鈕點擊事件
    //因為新增文章按鈕是後來用AJAX取回產生，故採用$(document).delegate
    $(document).delegate('#CreateArticleModal #createBtn', 'click', function () {
        $('#CreateArticleModal form').submit();
    });
});


//文章新增表單送出處理成功完成後事件
//此處function名稱需與AjaxForm上的OnSuccess定義內容一致
function onArticleModalCreateSucess() {
    //關閉跳窗
    $('#CreateArticleModal').modal('hide');
    //清空過濾條件輸入
    $('#Search').val(null);
    //點擊重整連結來重新載入列表
    $('#refeshListLink').click();
}

