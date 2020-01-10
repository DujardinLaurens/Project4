<script>
	import { onMount } from 'svelte';
	import Header from './slots/header.svelte';

	let beers = [];
	
	const pages = [
		...new Array(24).fill().map((x, i) => i).slice(1)
	]

	let beerId = []
	
	async function gotoPage (page) {
		console.log(`go to page ${page}`)
		const res = await fetch(`https://sandbox-api.brewerydb.com/v2/beers?p=${page}&key=395c2bade2ee114e421a9228d3cbc512`);
		const json = await res.json();
		beers = json.data;
	};

	onMount(async () => {
		const res = await fetch(`https://sandbox-api.brewerydb.com/v2/beers?p=1&key=395c2bade2ee114e421a9228d3cbc512`);
		const json = await res.json();
		beers = json.data;
		console.log(beers);
	});
</script>

<main>
	<Header></Header>
	<div class="gallery">
		{#each beers as beer}
		<div class="beer_gallery">
			{#if beer.labels == undefined}
				<div class="beer_photo">
					<img src="beer.png" alt="image"/>
				</div>
			{:else}
				<div class="beer_photo">
					<img src="{beer.labels.medium}" alt="image" />
				</div>
			{/if}
			<div class="beer_name">
				<p>{beer.name}</p>
				<p>{beer.id}</p>
			</div>
		</div>
		{/each}
	</div>

	<button on:click={() => gotoPage(0)}>
		&lt;
	</button>
	{#each pages as page}
		<button on:click={() => gotoPage(page)}>
			{page}
		</button>
	{/each}
	<button on:click={() => gotoPage(pages.length)}>
		&gt;
	</button>
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
	.gallery {
		max-width: 100%;
		margin: 0 auto;
	}
	.beer_gallery {
		width: calc(20% - 60px);
		display: inline-grid;
		padding: 10px;
		margin: 20px;
	}

	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}
</style>