
$(document).ready(function () {
    // Function to check if an element is in the viewport
    function isInViewport(element) {
      var elementTop = $(element).offset().top;
      var elementBottom = elementTop + $(element).outerHeight();
      var viewportTop = $(window).scrollTop();
      var viewportBottom = viewportTop + $(window).height();
      return elementBottom > viewportTop && elementTop < viewportBottom;
    }
  
    // Initialize counters for each element
    $('.counter-count').each(function () {
      var $this = $(this);
      var hasAnimated = false; // Flag to prevent multiple animations
  
      // Scroll event listener
      $(window).on('scroll', function () {
        if (isInViewport($this) && !hasAnimated) {
          $this.prop('Counter', 0).animate({
            Counter: $this.text()
          }, {
            duration: 5000,
            easing: 'swing',
            step: function (now) {
              $this.text(Math.ceil(now));
            }
          });
          hasAnimated = true; // Prevent re-triggering
        }
      });
  
      // Trigger scroll event on page load for elements already in view
      if (isInViewport($this)) {
        $(window).trigger('scroll');
      }
    });
  });

  function capt() {
    let strarr = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
    let captcha = "";
    for (let i = 0; i < 5; i++) {
        captcha = captcha + strarr[Math.floor(Math.random() * strarr.length)];
    }
    return captcha;
}
function showCaptcha() {
    let captcha = capt();
    document.getElementById("ct1").innerHTML = captcha;
    document.getElementById("hdnct1").value = captcha;
}
$(document).ready(function(){
    showCaptcha();
});  