﻿var Helper = {
    Module: {
        Auto: function () {
            var modules = $("[id^=Module-]");
            for (var i = 0; i < modules.length; i++) {
                var module = modules[i];
                var id = module.id;
                var moduleName = id.replace("Module-", "");
                Helper.Module.Init(moduleName);
            }
        },
        Init: function (name) {
            $.ajax({
                ModuleName: name,
                type: "GET",
                url: "/module/" + name,
                data: [],
                success: Helper.Module.Init_Callback,
                dataType: "html",
                contentType: "html"
            });
        },
        Init_Callback: function (result) {
            $("#Module-" + this.ModuleName).html(result);
        }
    },
    UI: {
        Alert: function (title, message, type) {
            swal.fire({
                title: title,
                text: message,
                type: type,
                buttonsStyling: false,
                confirmButtonText: "Tamam",
                confirmButtonClass: "btn btn-brand"
            });
        }
    },

    MailCheck: function (email) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return regex.test(email);
    }
};