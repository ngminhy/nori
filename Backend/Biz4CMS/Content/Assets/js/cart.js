jQuery(document).ready(function($){

	var cartWrapper = $('.cd-cart-container');
	var productId = 0;
	if( cartWrapper.length > 0 ) {
		var cartBody = cartWrapper.find('.body')
		var cartList = cartBody.find('ul').eq(0);
		var cartTotal = cartWrapper.find('.checkout').find('span');
		var cartTrigger = cartWrapper.children('.cd-cart-trigger');
		var cartCount = cartTrigger.children('.count')
		var addToCartBtn = $('.btn-addcart');

		//add product to cart
		addToCartBtn.on('click', function(event){
			event.preventDefault();
			addToCart($(this));
		});

		//open/close cart
		cartTrigger.on('click', function(event){
			event.preventDefault();
			toggleCart();
		});

		//close cart when clicking on the .cd-cart-container::before (bg layer)
		cartWrapper.on('click', function(event){
			if( $(event.target).is($(this)) ) toggleCart(true);
		});
	}

	function toggleCart(bool) {
		var cartIsOpen = ( typeof bool === 'undefined' ) ? cartWrapper.hasClass('cart-open') : bool;
		
		if( cartIsOpen ) {
			cartWrapper.removeClass('cart-open');
			setTimeout(function(){
				cartBody.scrollTop(0);
				//check if cart empty to hide it
				if( Number(cartCount.find('li').eq(0).text()) == 0) cartWrapper.addClass('empty');
			}, 500);
		} else {
			cartWrapper.addClass('cart-open');
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
		var cartIsEmpty = cartWrapper.hasClass('empty');
		// //update cart product list
		addProduct(prouctItem);

		// //update number of items 
		// updateCartCount(cartIsEmpty);
		// // //update total price
		// updateCartTotal(trigger.data('price'), true);
		// // //show cart
		cartWrapper.removeClass('empty');
	}

	function addProduct(prouctItem) {
		var name = prouctItem.name;
		var img = prouctItem.img.replace('~', '');
		var price = numeral(prouctItem.price).format('0,0');
		var count = prouctItem.count;

		console.log(prouctItem);

		var productAdded = $('<li class="product">'+
			'<div class="product-image">'+
			'<a href="#"><img src="'+img+'"></a></div>'+
			'<div class="product-details">'+
			'<h5><a href="">'+name+'</a></h5>'+
			'<div class="actions"><span class="qty">'+count+'</span> x <span class="price">'+price+' VNĐ</span></div>'+
			'</div></li>');
		cartList.prepend(productAdded);
	}

	// function quickUpdateCart() {
	// 	var quantity = 0;
	// 	var price = 0;
		
	// 	cartList.children('li:not(.deleted)').each(function(){
	// 		var singleQuantity = Number($(this).find('select').val());
	// 		quantity = quantity + singleQuantity;
	// 		price = price + singleQuantity*Number($(this).find('.price').text().replace('$', ''));
	// 	});

	// 	cartTotal.text(price.toFixed(2));
	// 	cartCount.find('li').eq(0).text(quantity);
	// 	cartCount.find('li').eq(1).text(quantity+1);
	// }

	function updateCartCount(emptyCart, quantity) {
		if( typeof quantity === 'undefined' ) {
			var actual = Number(cartCount.find('li').eq(0).text()) + 1;
			var next = actual + 1;
			
			if( emptyCart ) {
				cartCount.find('li').eq(0).text(actual);
				cartCount.find('li').eq(1).text(next);
			} else {
				cartCount.addClass('update-count');

				setTimeout(function() {
					cartCount.find('li').eq(0).text(actual);
				}, 150);

				setTimeout(function() {
					cartCount.removeClass('update-count');
				}, 200);

				setTimeout(function() {
					cartCount.find('li').eq(1).text(next);
				}, 230);
			}
		} else {
			var actual = Number(cartCount.find('li').eq(0).text()) + quantity;
			var next = actual + 1;
			
			cartCount.find('li').eq(0).text(actual);
			cartCount.find('li').eq(1).text(next);
		}
	}

	function updateCartTotal(price, bool) {
		bool ? cartTotal.text( (Number(cartTotal.text()) + Number(price)).toFixed(2) )  : cartTotal.text( (Number(cartTotal.text()) - Number(price)).toFixed(2) );
	}
});