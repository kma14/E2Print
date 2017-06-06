/// <reference path="jquery-1.10.2.min.js" />
$(document).ready(function () {
    RoleTable();
    UserTable();
    ProductTable();
    CategoryTable();
    PromotionTable();
    if ($(document).find("title").text() == "Manage Feedbacks") {
        FeedbackTable();
    }
    ProductTabs();
});

function RoleTable() {
    $('#RoleTable').jtable({
        title: 'The Role List',
        actions: {
            listAction: '/Role/RoleList',
            deleteAction: '/Role/Delete',
            updateAction: '/Role/Update',
            createAction: '/Role/Create'
        },
        fields: {
            Phones: {
                title: '',
                width: '1%',
                sorting: false,
                edit: false,
                create: false,
                display: function (roleData) {
                    var $childTabelSwitch = $('<span class="fontawesome-list"></span>');
                    $childTabelSwitch.on('click', function () {
                        $('#RoleTable').jtable('openChildTable', $childTabelSwitch.closest('tr'), {
                            title: roleData.record.RoleName + ' users',
                            actions: {
                                listAction: '/Customer/GetCustomersByRole?RoleName=' + roleData.record.RoleName,
                                deleteAction: '/Role/RemoveUserFromRole',
                                updateAction: '/Role/UpdatePhone',
                                createAction: '/Role/RemoveUserFromRole'
                            },
                            fields: {
                                RoleNameBlah: {
                                    type: 'hidden',
                                    defaultValue: roleData.record.RoleName
                                },
                                Id: {
                                    key: true,
                                    create: false,
                                    edit: false,
                                    list: false,
                                },
                                Login: {
                                    title: 'Login',
                                    type: 'string',
                                    create: false,
                                    edit: false
                                },
                                FirstName: {
                                    title: 'FirstName',
                                    type: 'string',
                                    create: false,
                                    edit: false
                                },
                                LastName: {
                                    title: 'LastName',
                                    type: 'string',
                                    create: false,
                                    edit: false
                                }
                            }
                        }, function (data) { //opened handler
                            data.childTable.jtable('load');
                        });
                    });
                    return $childTabelSwitch;
                }
            },
            Id: {
                key: true,
                create: false,
                edit: false
            },
            RoleName: {
                title: 'Role name',
                width: '15%'
            },
            Discount: {
                title: 'Discount'
            }
        }
    });

    $('#RoleTable').jtable('load');
}

function UserTable() {
    $('#UserTable').jtable({
        title: 'The User List',
        actions: {
            listAction: '/Customer/GetUserList',
            deleteAction: '/Customer/Delete',
            updateAction: '/Customer/UpdateUser',
            createAction: '/Customer/Create'
        },
        fields: {
            //string contactNumber, string address, string email, string company = "",string role=""
            Id: {
                key: true,
                create: false,
                edit: false,
                list: false,
            },
            Login: {
                title: 'Login',
                type: 'string',
                create: false,
                edit: false
            },
            Login: {
                type: 'hidden',
            },
            Password: {
                type: 'hidden'
            },
            FirstName: {
                title: 'FirstName',
                type: 'string',
                create: false,
            },
            LastName: {
                title: 'LastName',
                type: 'string',
                create: false,
            },
            Email: {
                title: 'Email',
                type: 'string',
                create: false,
            },
            Role: {
                title: 'Role',
                type: 'string',
                create: false,
                options: '/Role/GetRoleList'
            }
        }
    });

    //Load person list from server
    $('#UserTable').jtable('load');
}

function ProductTable() {
    $('#ProductTable').jtable({
        title: 'The Product List',
        actions: {
            //listAction: '/Product/GetProductList',
            //listAction: function (postData, jtParams) {
            //    console.log("Loading product data...");
            //    return $.Deferred(function ($dfd) {
            //        $.ajax({
            //            url: '/Product/GetProductList' ,
            //            type: 'Post',
            //            dataType: 'json',
            //            data: null,
            //            success: function (data) {
            //                $dfd.resolve(data);
            //                console.clear();
            //            },
            //            error: function () {
            //                $dfd.reject();
            //            }
            //        });
            //    });
            //},
            listAction: '/Product/GetProductList',
            deleteAction: '/Product/DeleteProduct',
            updateAction: '/Product/UpdateProduct',
            createAction: '/Product/CreateProduct'
        },
        paging: true,
        pageSize: 10,
        fields: {
            Id: {
                key: true,
                create: false,
                edit: false,
                list: false,
            },
            //Category: {
            //    title:"Category",
            //    display: function (productData) {
            //        return "<a class=\"dark\" href=/Category/ManageCategory/" + productData.record.Category.id+ ">" + productData.record.Category.Name + "</a>";
            //    },
            //    create: false,
            //    edit: false,
            //    width:'20%'
            //},
            //CategoryId: {
            //    title: "Category",
            //    list:false,
            //    options: function (data) {
            //        if (data.source == 'create') {
            //            return '/Category/GetLeafCategories_DropDownList'
            //        } else if (data.source == 'edit') {
            //            return '/Category/GetLeafCategories_DropDownList'
            //        }
            //    }
            //},
            CategoryId: {
                title: "CategoryId",
                list:false,
                options: '/Category/GetLeafCategories_DropDownList'
            },
            CategoryName: {
                title: "Category",
                edit: false,
                create: false,
                width: '20%'
            },
            Size: {
                title: 'Size',
                type: 'string',
                width:'25%'
            },
            Color: {
                title: 'Color',
                type: 'string',
                width: '25%'
            },
            Material: {
                title: 'Material',
                type: 'string',
                width: '20%'
            },
            BuyingQty: {
                title: 'Qty',
                type: 'int',
                width: '5%'
            },
            Price: {
                title: 'Price',
                type: 'decimal',
                width: '5%'
            }
        }
    });
    $('#ProductTable').jtable('load');
}

function CategoryTable() {
    $('#CategoryTable').jtable({
        title: 'The Category List',
        actions: {
            listAction: '/Category/GetAllCategories',
            deleteAction: '/Category/DeleteCategory',
            updateAction: '/Category/UpdateCategory',
            createAction: '/Category/CreateCategory'
        },
        paging: true,
        pageSize :10,
        fields: {
            Id: {
                key: true,
                create: false,
                edit: false,
                list: false,
            },
            Name: {
                title: "Name",
                type: 'string',
                width: '20%'
            },
            Description: {
                title: "Description",
                type: 'string',
                width: '60%'
            },
            ParentId: {
                title: 'Parent Category',
                type: 'string',
                options: '/Category/GetCategories_DropDownList',
                width: '20%'
            }
        }
    });
    $('#CategoryTable').jtable('load');
}

function PromotionTable() {
    $('#PromotionTable').jtable({
        title: 'The Promotion List',
        actions: {
            listAction: '/Promotion/GetAllPromotions',
            deleteAction: '/Promotion/DeletePromotion',
            updateAction: '/Promotion/UpdatePromotion',
            createAction: '/Promotion/CreatePromotion'
        },
        paging: true,
        pageSize: 10,
        fields: {
            Id: {
                key: true,
                create: false,
                edit: false,
                list: false,
            },
            ItemId: {
                title: "ItemId",
                list: false,
                options: '/Category/GetLeafCategories_DropDownList'
            },
            ItemName: {
                title: "ItemName",
                edit: false,
                create: false,
                width: '10%'
            },
            Title: {
                title: "Title",
                type: 'string',
                width: '10%'
            },
            Description: {
                title: "Description",
                type: 'string',
                width: '30%'
            },
            StartDate: {
                title: 'StartDate',
                display: function (data) {
                    var jsonDateString = data.record.StartDate;
                    if (jsonDateString.indexOf("Date") > 0) {
                        //http://stackoverflow.com/questions/1244094/converting-json-results-to-a-date
                        var myDate = new Date(jsonDateString.match(/\d+/)[0] * 1);
                        return myDate.getFullYear() + '-' + ('0' + (myDate.getMonth() + 1)).slice(-2) + '-' + ('0' + myDate.getDate()).slice(-2);
                    } else {
                        return jsonDateString
                    }
                },
                input: function (data) {
                    if (data.record) { //edit form
                        var myDate = new Date(data.value.match(/\d+/)[0] * 1);
                        var dateString = myDate.getFullYear() + '-' + ('0' + (myDate.getMonth() + 1)).slice(-2) + '-' + ('0' + myDate.getDate()).slice(-2);
                        return '<input type="text" name="StartDate" style="width:181px" value="' + dateString + '" />';
                    } else { //create form
                        return '<input type="text" name="StartDate" style="width:181px" value="" />';
                    }
                },
                width:'20%'
            },
            EndDate: {
                title: 'EndDate',
                display: function (data) {
                    var jsonDateString = data.record.EndDate;
                    if (jsonDateString.indexOf("Date") > 0) {
                        var myDate = new Date(jsonDateString.match(/\d+/)[0] * 1);
                        return myDate.getFullYear() + '-' + ('0' + (myDate.getMonth() + 1)).slice(-2) + '-' + ('0' + myDate.getDate()).slice(-2);
                    } else {
                        return jsonDateString
                    }
                },
                input: function (data) {
                    if (data.record) { //edit form
                        var myDate = new Date(data.value.match(/\d+/)[0] * 1);
                        var dateString = myDate.getFullYear() + '-' + ('0' + (myDate.getMonth() + 1)).slice(-2) + '-' + ('0' + myDate.getDate()).slice(-2);
                        return '<input type="text" name="EndDate" style="width:181px" value="' + dateString + '" />';
                    } else { //create form
                        return '<input type="text" name="EndDate" style="width:181px" value="" />';
                    }
                },
                width: '20%'
            },
            DiscountAmount: {
                title: "Discount",
                type: 'decimal',
                width: '10%'

            },
            PromotionPrice: {
                title: "Price",
                type: 'decimal',
                width: '10%'
            }

        }
    });
    $('#PromotionTable').jtable('load');
}

function FeedbackTable() {
    $('#FeedbackTable').jtable({
        title: 'The Feedback List',
        actions: {
            listAction: '/Feedback/GetAll',
            deleteAction: '/Feedback/Delete',
            updateAction: '/Feedback/Update',
            createAction: '/Feedback/Create'
        },
        paging: true,
        pageSize: 10,
        fields: {
            Id: {
                key: true,
                create: false,
                edit: false,
                list: false,
            },
            CustomerName: {
                title: "Customer",
                width: '20%'
            },
            CommentDate: {
                title: "Date",
                display: function (data) {
                    var jsonDateString = data.record.CommentDate;
                    if (jsonDateString.indexOf("Date") > 0) {
                        //http://stackoverflow.com/questions/1244094/converting-json-results-to-a-date
                        var myDate = new Date(jsonDateString.match(/\d+/)[0] * 1);
                        return myDate.getFullYear() + '-' + ('0' + (myDate.getMonth() + 1)).slice(-2) + '-' + ('0' + myDate.getDate()).slice(-2);
                    } else {
                        return jsonDateString
                    }
                },
                width: '10%',
                create: false,
                edit: false,
            },
            Comment: {
                title: "Comment",
                input: function (data) {
                    if (data.record) { //if data's record property is not undefined, it is in edit form
                        return '<textarea name="Comment" rows="7" style="width:400px">' + data.value + '</textarea>';
                    } else { //create form
                        return '<textarea name="Comment" rows="7" style="width:400px"></textarea>'
                    }
                },
                width: '70%'
            },
            Photo: {
                title: "Comment",
                display:function(data){
                    return '<img class="manageFeedback-photo" src="/Content/Images/Feedbacks/' + data.record.Id + '.jpg"/>'
                },
                input: function (data) {
                    if (data.record) { //if data's record property is not undefined, it is in edit form
                        return '<img class="manageFeedback-photo" src="/Content/Images/Feedbacks/' + data.record.Id + '.jpg"/>' +
                                '<div class="feedbackPhoto-fileUploadContainer">' +
                                    '<span class="btn btn-success fileinput-button">' +
                                    '<i class="glyphicon glyphicon-plus"></i>' +
                                    '<span><b>feedback photo:</b></span>' +
                                    '<input id="' + data.record.Id + '" class="fileupload" type="file" name="files[]" data-url="/Photo/UploadPhoto/" multiple><input type="hidden" name="Photo" value="'+data.record.Photo+'" />' +
                                    '</span></br></br>' +
                                    '<div class="progress">' +
                                        '<div class="progress-bar"></div>' +
                                    '</div>' +
                                '</div>';
                    } else {
                        return '<div class="feedbackPhoto-fileUploadContainer">' +
                                    '<span class="btn btn-success fileinput-button">' +
                                    '<i class="glyphicon glyphicon-plus"></i>' +
                                    '<span><b>feedback photo:</b></span>' +
                                    '<input class="fileupload" type="file" name="files[]" data-url="/Photo/UploadPhoto/" multiple>' +
                                    '</span><br />' +
                                    '<div class="progress">' +
                                        '<div class="progress-bar"></div>' +
                                    '</div>' +
                                '</div>';
                    }
                },
                width: '70%'
            },

        }
    });
    $('#FeedbackTable').jtable('load');
    $(document).on('click', '.feedbackPhoto-fileUploadContainer', function () {      
        EnablingFeedbackPhotoUpload($(this));
    });
}

function EnablingFeedbackPhotoUpload(fileuploadContainer) {
    var feedbackId = fileuploadContainer.find('input.fileupload').attr('id');
    var photoValue = fileuploadContainer.find('input[name="Photo"]');
    var imgPreview = fileuploadContainer.siblings('.manageFeedback-photo');
    'use strict';
    // Change this to the location of your server-side upload handler:
    $('.fileupload').fileupload({
        //dataType: 'json',  //with this line done is not firing
        add: function (e, data) {
            data.formData = { 'photoType': '6', 'feedbackId': feedbackId };
            data.submit();
        },
        done: function (e, data) {
            var d = new Date();
            photoValue.val('/Content/Images/Feedbacks/' + feedbackId + '.jpg');
            imgPreview.attr('src', photoValue.val()+'?'+d.getTime());
            //$.each(data.result.files, function (index, file) {
            //    $('<p/>').text(file.name).appendTo('#files');
            //});
        },
        progressall: function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            $('.progress .progress-bar').css(
                'width', progress + '%'
            );
        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
}

function ProductTabs() {
    $("#tabs").tabs();
    $('span#attrName-lable').text('Please selecet an attribute');
    $('#productAttributes').on('click', '#addAttrName', function () {
        var newAttriName = $(this).siblings('#newAttrName').val();
        $.post('/Product/CreateProductAttribute/', { AttributeName: newAttriName, AttributeValue: 'default', Description: '' }, function (data) {
            if (data.Result == 'OK') {
                $('ul#attrNameList').append('<li class="attrnames">' + data.Record.AttributeName + ' <span class="productAttributes-delete fontawesome-remove"></span></li>');
                $('input#newAttrName').val('');
                var attributes = JSON.parse($('input#prudutAttrs').val());
                attributes.push({ "Id": data.Record.Id, "AttributeName": data.Record.AttributeName, "AttributeValue": "default", "Description": null });
                $('input#prudutAttrs').val(JSON.stringify(attributes));
            }
        });
    });
    $('#productAttributes').on('click', '#addAttrValue', function () {
        var newAttriName = $('ul#attrNameList li.currentAttribute').text().trim();
        var newAttriValue = $(this).siblings('#newAttrValue').val();
        
        $.post('/Product/CreateProductAttribute/', { AttributeName: newAttriName, AttributeValue: newAttriValue, Description: '' }, function (data) {
            if (data.Result == 'OK') {
                $('ul#attrValueList').append('<li class="attrValues" id="' + data.Record.Id + '">' + data.Record.AttributeValue + '<span class="productValues-delete fontawesome-remove"></span></li>');
                $('input#newAttrValue').val('');

                var attributes = JSON.parse($('input#prudutAttrs').val());
                attributes.push({ "Id": data.Record.Id, "AttributeName": data.Record.AttributeName, "AttributeValue": data.Record.AttributeValue, "Description": null });
                $('input#prudutAttrs').val(JSON.stringify(attributes));
            }
        });
    });
    $('#productAttributes').on('click', 'span.productAttributes-delete', function (e) {
        event.stopPropagation();
        var elementToDelete = $(this).parent('li');
        var attrName = $(this).parent('li').text().trim();
        var confirmed = confirm("Are your sure to delete attribute [" + attrName + "]?");
        if (confirmed) {
            $.post('/Product/DeleteProductAttributeByName/', { AttributeName: attrName }, function (data) {
                elementToDelete.remove();
                $('ul#attrValueList').html('');
                $('span#attrName-lable').text('Please selecet an attribute');
                var attributes = JSON.parse($('input#prudutAttrs').val());
                var newAttriArray = $.grep(attributes, function (item, index) {
                    return item.AttributeName.trim().toLowerCase() != attrName.trim().toLowerCase();
                });
                $('input#prudutAttrs').val(JSON.stringify(newAttriArray));
            });
        }
    });

    $('#productAttributes').on('click', 'span.productValues-delete', function (e) {
        event.stopPropagation();
        var elementToDelete = $(this).parent('li');
        var attrId = $(this).parent('li').attr('id');
        $.post('/Product/DeleteProductAttributeById/', { AttributeId: attrId }, function (data) {
            if (data.Result == 'OK') {
                elementToDelete.remove();
                var attributes = JSON.parse($('input#prudutAttrs').val());
                var newAttriArray = $.grep(attributes, function (item, index) {
                    return item.Id != attrId;
                });
                $('input#prudutAttrs').val(JSON.stringify(newAttriArray));
            } else {
                alert(data.Message)
            }
        });
    });

    $('#productAttributes').on('click', 'li.attrnames', function (e) {
        event.stopPropagation();
        $('ul#attrNameList li.attrnames').not($(this)).removeClass('currentAttribute');
        $(this).addClass('currentAttribute');
        var attrName = $(this).text().trim();
        var attributes = JSON.parse($('input#prudutAttrs').val());
        $('span#attrName-lable').text(attrName + ' Values');
        $('ul#attrValueList').html('');
        $.each(attributes, function (index, item) {
            if (item.AttributeName.trim().toLowerCase() == attrName.toLowerCase() && item.AttributeValue.trim() != 'default') {
                $('ul#attrValueList').append('<li class="attrValues" id="' + item.Id + '">' + item.AttributeValue + '<span class="productValues-delete fontawesome-remove"></span></li>');
            }
        });
    });
}