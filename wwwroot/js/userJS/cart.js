function updateCartItem(obj, rowid, url) {
	$.get(
		url, { rowid: rowid, qty: obj.value },
		function (res) {
			if (res == 'ok') {
				location.reload();
			} else {
				alert('Cart update quantity failed, Please try again');
			}

		}
	)
}
