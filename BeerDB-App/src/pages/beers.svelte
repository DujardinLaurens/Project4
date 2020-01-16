<script>
	import Header from './slots/header.svelte';
	import Loader from './slots/loader.svelte';
	import { url } from '@sveltech/routify';

	let beers = [];
	
	const pages = [
		...new Array(24).fill().map((x, i) => i).slice(1)
	]

	let promise = getBeers();
	
	async function gotoPage (page) {
		console.log(`go to page ${page}`)
		const res = await fetch(`https://sandbox-api.brewerydb.com/v2/beers?p=${page}&key=395c2bade2ee114e421a9228d3cbc512`);
		const json = await res.json();
		beers = json.data;
	};

	async function getBeers(){
		const res = await fetch(`https://sandbox-api.brewerydb.com/v2/beers?p=1&key=395c2bade2ee114e421a9228d3cbc512`);
		const json = await res.json();
		beers = json.data;
		console.log(beers);
	};
</script>

<main>
	<Header></Header>
	{#await promise}
	<Loader></Loader>
	{:then}
	<div id="myTable" class="gallery">
		{#each beers as beer}
		<a href={$url('/beer/:beerId', {beerId: `${beer.id}`})}><button id="btn_none" class="btn_none">
		<div id="beer_gallery" class="beer_gallery">
			{#if beer.labels == undefined}
				<div class="beer_photo">
					<img src="beer.png" alt="image"/>
				</div>
			{:else}
				<div class="beer_photo">
					<img src="{beer.labels.medium}" alt="image" />
				</div>
			{/if}
			<div id="name" class="beer_name">
				<p>{beer.name}</p>
			</div>
		</div></button></a>
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
	{/await}
</main>

<style>
	main {
		text-align: center;
		max-width: 240px;
		margin: 0 auto;
	}
	.gallery {
		max-width: 100%;
		margin: 0 auto;
	}
	.btn_none {
		width: calc(20% - 60px);
		display: inline-grid;
		padding: 10px;
		margin: 20px;
		background: transparent;
		border: transparent;
	}
	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}
</style>