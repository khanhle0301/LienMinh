
  
/*
 * sticky
 */  

$(window).load(function(){      
	$('.sticky').scrollToFixed({
		marginTop: $('header').outerHeight(true) + -120,
		limit: function() {
			var limit = $('footer').offset().top - $('.sticky').outerHeight(true) - 10;
			return limit;
		},
		fixed: function() {  },
		dontCheckForPositionFixedSupport: true
	});
  
	/*
	 * scroll
	 */  	
	$('.listheroes').slimScroll({
		 width: 'auto',
		 height: '325px',  
		 color: '#666', 
			railVisible: true
	});
});