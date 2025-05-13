
$(document).ready(function () {
   
    $(document).on('submit', '.ajax-form', function (e) {
        e.preventDefault();

        var form = $(this);
        var url = form.attr('action');
        var formData = form.serialize();
        var token = $('input:hidden[name="__RequestVerificationToken"]').val();

        $.ajax({
            type: "POST",
            url: url,
            data: formData,
            headers: {
                "RequestVerificationToken": token
            },
            success: function (response) {
                if (response.success) {
                    
                    if (response.message) {
                        showAlert('success', response.message);
                    }

                   
                    if (response.redirectUrl) {
                        setTimeout(function () {
                            window.location.href = response.redirectUrl;
                        }, 1500);
                    }

                   
                    if (response.updateTable) {
                        $('#ogrenciTablosu').html(response.tableHtml);
                    }
                } else {
                   
                    showAlert('danger', response.message || 'İşlem sırasında bir hata oluştu.');
                }
            },
            error: function () {
                showAlert('danger', 'Sunucu ile iletişim sırasında bir hata oluştu.');
            }
        });
    });

   
    $(document).on('click', '.ajax-delete', function (e) {
        e.preventDefault();

        if (confirm('Bu öğrenciyi silmek istediğinizden emin misiniz?')) {
            var url = $(this).attr('href');
            var token = $('input:hidden[name="__RequestVerificationToken"]').val();

            $.ajax({
                type: "POST",
                url: url,
                headers: {
                    "RequestVerificationToken": token
                },
                success: function (response) {
                    if (response.success) {
                        showAlert('success', response.message);

                       
                        if (response.updateTable) {
                            $('#ogrenciTablosu').html(response.tableHtml);
                        }
                    } else {
                        showAlert('danger', response.message || 'Silme işlemi sırasında bir hata oluştu.');
                    }
                },
                error: function () {
                    showAlert('danger', 'Sunucu ile iletişim sırasında bir hata oluştu.');
                }
            });
        }
    });

   
    $(document).on('click', '.ajax-detail', function (e) {
        e.preventDefault();

        var url = $(this).attr('href');

        $.ajax({
            type: "GET",
            url: url,
            success: function (response) {
                $('#detayModalBody').html(response);
                $('#detayModal').modal('show');
            },
            error: function () {
                showAlert('danger', 'Detay bilgisi alınırken bir hata oluştu.');
            }
        });
    });
});


function showAlert(type, message) {
    var alertDiv = $('<div class="alert alert-' + type + ' alert-dismissible fade show" role="alert">' +
        message +
        '<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>' +
        '</div>');

   
    if ($('#alertContainer').length) {
        $('#alertContainer').html(alertDiv);
    } else {
        // Yoksa sayfanın üstüne ekle
        $('.container:first').prepend(alertDiv);
    }

  
    setTimeout(function () {
        alertDiv.alert('close');
    }, 5000);
}
