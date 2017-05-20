jQuery(document).ready(function($){
	var $cartWrapper = $('.cd-cart-container');
	var $cartBody = $cartWrapper.find('.body')
	var $cartList = $cartBody.find('ul').eq(0);
	var $cartTrigger = $cartWrapper.children('.cd-cart-trigger');
	var $cartCount = $cartTrigger.children('.count')
	var $addToCartBtn = $('.btn-addcart');
	$addToCartBtn.on('click', function(event){
		event.preventDefault();
		addToCart($(this));
	});
	$cartTrigger.on('click', function(event){
		event.preventDefault();
		toggleCart();
	});
	$cartWrapper.on('click', function(event){
		if( $(event.target).is($(this)) ) toggleCart(true);
	});
	if(localStorage.length == 0) {
		$cartWrapper.addClass('empty')
	} else {
		$cartWrapper.removeClass('empty');
		initProduct();
	};
	function toggleCart(boolean) {
		var cartIsOpen = ( typeof boolean === 'undefined' ) ? $cartWrapper.hasClass('cart-open') : boolean;
		if( cartIsOpen) {
			$cartWrapper.removeClass('cart-open');
			setTimeout(function(){
				$cartBody.scrollTop(0);
				if(localStorage.length == 0) $cartWrapper.addClass('empty');
			}, 500);
		} else {
			$cartWrapper.addClass('cart-open');
		}
	}
	function addToCart(trigger) {
		var prouctItem = {
		    id: trigger.attr('data-product-id'),
		    name: trigger.attr('data-product-name'),
		    price: trigger.attr('data-product-price'),
		    img: trigger.attr('data-product-img'),
		    count: 0
		}
		var count = 0;
		var itemStorage = JSON.parse(localStorage.getItem(prouctItem.id));
		if (itemStorage != null) {
		    count = JSON.parse(localStorage.getItem(prouctItem.id)).count;
		}
		count ++;
		prouctItem.count = count;
		localStorage.setItem(prouctItem.id, JSON.stringify(prouctItem));
		initProduct();
	}
	function initProduct() {
		$cartList.html("");
		Object.keys(localStorage).forEach(function (key) {
			var itemlocal = JSON.parse(localStorage.getItem(key));
			var name = itemlocal.name;
			var img = itemlocal.img.replace('~', '');
			var price = numeral(itemlocal.price).format('0,0');
			var count = itemlocal.count;
			var productHTML = $('<li class="product">'+
				'<div class="product-image">'+
				'<a href="#"><img src="'+img+'"></a></div>'+
				'<div class="product-details">'+
				'<h5><a href="">'+name+'</a></h5>'+
				'<div class="actions"><span class="qty">'+count+'</span> x <span class="price">'+price+' VNĐ</span></div>'+
				'</div></li>');
			$cartList.prepend(productHTML);
		})
		if(localStorage.length != 0) $cartWrapper.removeClass('empty');
		updateCartCount();
	}
	function updateCartCount() {
		if(localStorage.length != 0) $cartCount.find('li').text(localStorage.length);
	}
});