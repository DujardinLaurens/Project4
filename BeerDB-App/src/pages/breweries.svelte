<script>
	import { onMount } from 'svelte';
	import Header from './slots/header.svelte';

	let breweries = [];

	onMount(async () => {
		const res = await fetch('https://sandbox-api.brewerydb.com/v2/breweries/?key=395c2bade2ee114e421a9228d3cbc512');
		const json = await res.json();
		breweries = json.data;
		console.log(breweries)
	});
</script>

<main>
	<Header></Header>
	<div class="gallery">
		{#each breweries as brewery}
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
		</div>
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
	.gallery {
		max-width: 100%;
		margin: 0 auto;
	}
	.brewery_gallery {
		width: calc(20% - 60px);
		display: inline-grid;
		padding: 10px;
		margin: 20px;
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