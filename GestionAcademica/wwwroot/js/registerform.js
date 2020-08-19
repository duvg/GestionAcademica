/* Fundraising Grader
*
* Generic Copyright, yadda yadd yadda
*
* Plug-ins: jQuery Validate, jQuery 
* Easing
*/

$(document).ready(function () {
    var current_fs, next_fs, previous_fs;
    var left, opacity, scale;
    var animating;

    $("#birthdate").datepicker();
    function validateForm() {
        
        $(".steps").validate({
            errorClass: 'invalid',
            errorElement: 'span',
            invalidHandler: function (event, validator) {

            },
            errorPlacement: function (error, element) {
                error.insertAfter(element.next('span').children());
            },
            highlight: function (element) {
                $(element).attr("placeholder", $(element).data('name') + ' es requerido');
                $(element).addClass('is-invalid');
                console.log("Entrando en el error");
                $(element).next('span').show();
            },
            unhighlight: function (element) {
                $(element).removeClass('is-invalid');
               
            }
        });
    }
    validateForm();
    
    $(".next").click(function () {
        validateForm();
        if ((!$('.steps').valid())) {
            return true;
        }
        if (animating) return false;
        animating = true;
        current_fs = $(this).parents(".card");
        current_fs.hide();
        next_fs = $(this).parents(".card").next();
        $("#progressbar li").eq($(".card").index(next_fs)).addClass("active");
        next_fs.show();
        current_fs.animate({
            opacity: 0
        }, {
            step: function (now, mx) {
                scale = 1 - (1 - now) * 0.2;
                left = (now * 50) + "%";
                opacity = 1 - now;
                current_fs.css({
                    'transform': 'scale(' + scale + ')'
                });
                next_fs.css({
                    'left': left,
                    'opacity': opacity
                });
            },
            duration: 800,
            complete: function () {
                current_fs.hide();
                animating = false;
            },
            easing: 'easeInOutExpo'
        });
    });
    $(".submit").click(function (e) {
        e.preventDefault();
        validateForm();
        if ((!$('.steps').valid())) {
            return false;
        }
        if (animating) return false;
        animating = true;
        current_fs = $(this).parents(".card");
        next_fs = $(this).parents(".card").next();
        $("#progressbar li").eq($(".card").index(next_fs)).addClass("active");
        next_fs.show();
        current_fs.animate({
            opacity: 0
        }, {
            step: function (now, mx) {
                scale = 1 - (1 - now) * 0.2;
                left = (now * 50) + "%";
                opacity = 1 - now;
                current_fs.css({
                    'transform': 'scale(' + scale + ')'
                });
                next_fs.css({
                    'left': left,
                    'opacity': opacity
                });
            },
            duration: 800,
            complete: function () {
                current_fs.hide();
                animating = false;
            },
            easing: 'easeInOutExpo'
        });

        $(".steps").submit();
    });
    $(".previous").click(function () {
        if (animating) return false;
        animating = true;
        current_fs = $(this).parents(".card");
        previous_fs = $(this).parents(".card").prev();
        $("#progressbar li").eq($(".card").index(current_fs)).removeClass("active");
        previous_fs.show();
        current_fs.animate({
            opacity: 0
        }, {
            step: function (now, mx) {
                scale = 0.8 + (1 - now) * 0.2;
                left = ((1 - now) * 50) + "%";
                opacity = 1 - now;
                current_fs.css({
                    'left': left
                });
                previous_fs.css({
                    'transform': 'scale(' + scale + ')',
                    'opacity': opacity
                });
            },
            duration: 800,
            complete: function () {
                current_fs.hide();
                animating = false;
            },
            easing: 'easeInOutExpo'
        });
    });
});
