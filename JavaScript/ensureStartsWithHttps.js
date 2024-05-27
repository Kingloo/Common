function ensureStartsWithHttps(value) {
	const http = "http://";
	const https = "https://";
	if (value.startsWith(https)) {
		return value
	} else if (value.startsWith(http)) {
		return [value.slice(0, 4), 's', value.slice(4)].join('')
	} else {
		return https + value
	}
}
