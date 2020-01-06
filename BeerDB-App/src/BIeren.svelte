<script>
	import { onMount } from 'svelte';

	let bieren = [];
	let page = '1';
	let pages = [1,2,3,4,5,6,7,8,9,10,11,12]

	onMount(async () => {
		const res = await fetch('https://sandbox-api.brewerydb.com/v2/beers?p='+ page +'&key=395c2bade2ee114e421a9228d3cbc512');
		const json = await res.json();
		bieren = json.data;
		console.log(bieren)
	});
</script>

<main>
	<div class="gallery">
		{#each bieren as bier}
			<p>{bier.name}</p>
		{/each}
	</div>
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