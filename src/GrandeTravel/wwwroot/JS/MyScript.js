$(document).ready(function () {
    var title = $("Title").text();
    if (title === "Grande Travel Landing Page") {
        $("body").css("backgroundColor", "#000");
        $("footer").addClass("hide");             
      
    }
    
    $("#shareIcons").jsSocials({
        showCount: "inside",
        showLabel: false,
        shares: [
            "twitter",
            "facebook",
            "googleplus",
            "linkedin"
           
            
        ]
    });

   

   

    $(".ratings").on('click', function () {
        $(".ratings").removeClass('selected-active');
        $(".ratings").removeClass('selected-secondary-active');
        $(this).addClass('selected-active');
        $(this).prevAll().addClass('selected-secondary-active');

    })
    $(".ratings").mouseenter(function () {
        $(".ratings").removeClass('active');
        $(".ratings").removeClass('secondary-active');
        $(this).addClass('active');
        $(this).prevAll().addClass('secondary-active');

    })
    $(".ratings").mouseleave(function () {
        $(".ratings").removeClass('active');
        $(".ratings").removeClass('secondary-active');
       
    })


    //Moved down, throws error if can't find packages...
    $('#autocomplete').autocomplete({
        lookup: packages,

    });
});
