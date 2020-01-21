<script>
	import Header from './slots/header.svelte';
	import Loader from './slots/loader.svelte';
	import { url } from '@sveltech/routify';

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
	<Header headerMessage={"A list of all breweries"} logoImage="beer.png"></Header>
	{#await promise}
	<Loader></Loader>
	{:then}
	<div class="gallery">
		{#each breweries as brewery}
		<a href={$url('/brewery/:breweryId', {breweryId: `${brewery.id}`})}><button class="btn_none">
		<div class="brewery_gallery">
			{#if brewery.images == undefined}
				<div class="brewery_image">
					<img src="brewery.png" alt="image"/>
				</div>
			{:else}
				<div class="brewery_image">
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
	.gallery {
		max-width: 100%;
		margin: 0 auto;
	}
	a {
		text-decoration: none;
	}
	.btn_none {
		width: calc(20% - 60px);
		display: inline-grid;
		padding: 10px;
		margin: 20px;
		background: transparent;
		border: transparent;
		cursor: pointer;
		opacity: 0.7;
	}

	.btn_none:hover {
		opacity: 1;
	}
	.brewery_image img {
		width: 196px;
		height: 196px;
	}
	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}
</style>