function submitFormAsync(elem, hasFiles = true, postSuccess = null, url = null)
{
    var btn = $(elem);
    var form = $(elem).closest("form");

    form.validate({
        errorPlacement: function(error, element)
        {
            var placement = $(element).data('error');
            if (placement)
            {
                $(placement).append(error);
            }
            else
            {
                error.insertAfter(element).addClass("text-danger");
            }
        }
    });

    if (!form.valid())
    {
        return;
    }

    if (url === null)
    {
        url = form.prop("action");
    }

    var contentType = "application/x-www-form-urlencoded";
    if (hasFiles)
    {
        contentType = "multipart/form-data";
    }

    spinButton(btn);
    try
    {
        form.ajaxSubmit({
            dataType: "json",
            type: "POST",
            url: url,
            processData: false, // Important!
            contentType: contentType,
            cache: false,
            success: function(response, status, xhr, $form)
            {
                stopButtonSpin(btn);
                if (response.status === "success")
                {
                    showMessage("success", response.message);
                    if (response.redirectUri)
                    {
                        window.location.assign(response.redirectUri);
                    }
                    if (postSuccess !== null)
                    {
                        postSuccess();
                    }
                }
                else
                {
                    stopButtonSpin(btn);
                    showMessage("error", response.message);
                }
            },
            error: function(response)
            {
                stopButtonSpin(btn);
                showMessage("error", response.message);
            }
        });
    } catch (ex)
    {
        stopButtonSpin(btn);
        showMessage(form,
            "error",
            "Something went wrong. Kindly refresh the page and try again. If the problem persists, contact an administrator.");
    }
}

function spinButton(btn)
{
    btn.addClass("kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light").attr("disabled", true);
}

function stopButtonSpin(btn)
{
    btn.removeClass("kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light").attr("disabled", false);
}

function showMessage(type, message)
{
    var title = "Labwox";
    swal.fire({
        title: title + " Management System",
        text: message ? message : "Something happened! Kindly try again later.",
        type: type,
        confirmButtonColor: "#3085d6",
        confirmButtonText: type === "success" ? "Ok" : "I'll check it now"
    });
}

function submitDatatableFormAsync(elem, datatable, url = null)
{
    submitFormAsync(elem,
        false,
        () =>
        {
            $(elem).closest(".datatableModal").modal("toggle");
            return datatable.reload();
        },
        url);
}