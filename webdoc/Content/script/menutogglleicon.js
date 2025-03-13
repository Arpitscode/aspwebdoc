
// for bar button and x mark button
xBtnHide=true;
function menutogglebtn() {
    if (xBtnHide) {
        document.getElementById("bar").style="display:none";  
        // Hides the barbtn
        document.getElementById("xmark").style="display:block;";    // Shows the xbtn
        xBtnHide = false;        // Change state
    } 
    else {
        document.getElementById("bar").style="display:block";        // Hides the barbtn
        document.getElementById("xmark").style="display:none !important";   // Shows the xbtn
       xBtnHide = true; 
    }
}

