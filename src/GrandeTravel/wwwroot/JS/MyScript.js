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
});
