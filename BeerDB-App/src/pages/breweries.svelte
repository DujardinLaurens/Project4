<script>
	import Header from './slots/header.svelte';
	import Loader from './slots/loader.svelte';
	import { id } from '../store.js';

	let breweries = [];

	let promise = getBreweries();

	async function getBreweries(){
		const res = await fetch('https://sandbox-api.brewerydb.com/v2/breweries/?key=395c2bade2ee114e421a9228d3cbc512');
		const json = await res.json();
		breweries = json.data;
		console.log(breweries)
	};
</script>

<main>
	<Header></Header>
	{#await promise}
	<Loader></Loader>
	{:then}
	<div class="gallery">
		{#each breweries as brewery}
		<a href="/brewery_info"><button class="btn_none" on:click={() => $id = brewery.id}>
		<div class="brewery_gallery">
			{#if brewery.images == undefined}
				<div class="no_image">
					<img src="brewery.png" alt="image"/>
				</div>
			{:else}
				<div class="brewery_photo">
					<img src="{brewery.images.squareMedium}" alt="image" />
				</div>
			{/if}
			<div class="brewery_name">
				<p>{brewery.name}</p>
			</div>
		</div></button></a>
		{/each}
	</div>
	{/await}
</main>

<style>
	main {
		text-align: center;
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
	.btn_none {
		width: calc(20% - 60px);
		display: inline-grid;
		padding: 10px;
		margin: 20px;
		background: transparent;
		border: transparent;
	}
	.no_image img {
		width: 256px;
		height: 256px;
	}
	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}
</style>