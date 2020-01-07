<script>
	import { onMount } from 'svelte';
	import Header from './slots/header.svelte';
	
	onMount(async () => {
		const res = await fetch('https://sandbox-api.brewerydb.com/v2/beers?p=1&key=395c2bade2ee114e421a9228d3cbc512');
		const json = await res.json();
		bieren = json.data;
		console.log(bieren)
	});
</script>

<main>
	<Header></Header>
	
</main>

<style>
	main {
		text-align: center;
		padding: 1em;
		max-width: 240px;
		margin: 0 auto;
	}
	h1 {
		color: #ff3e00;
		text-transform: uppercase;
		font-size: 4em;
		font-weight: 100;
	}

	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}
</style>