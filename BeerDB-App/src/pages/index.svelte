<script>
    import Header from './slots/header.svelte';
	import Loader from './slots/loader.svelte';
	import { id } from '../store.js';

    let randomBeers = [];
    let randomBreweries = [];

    let promise = getRandomBeers();
    let prom = getRandomBreweries();

	async function getRandomBeers(){
        for(let i = 0; i < 10; i++) {
            const res = await fetch(`https://sandbox-api.brewerydb.com/v2/beer/random?p=1&key=395c2bade2ee114e421a9228d3cbc512`);
            const json = await res.json();
            randomBeers.push(json.data);
            console.log(randomBeers);
        }
    };
    
    async function getRandomBreweries(){
        for(let i = 0; i < 10; i++) {
            const res = await fetch(`https://sandbox-api.brewerydb.com/v2/brewery/random?p=1&key=395c2bade2ee114e421a9228d3cbc512`);
            const json = await res.json();
            randomBreweries.push(json.data);
            console.log(randomBreweries);
        }
	};
</script>

<main>
    <Header></Header>
	{#await promise}
	<Loader></Loader>
	{:then}
	<div class="gallery">
		{#each randomBeers as randomBeer}
		<a href="/beer_info"><button class="btn_none" on:click={() => $id = randomBeer.id}>
		<div class="beer_gallery">
			{#if randomBeer.labels == undefined}
				<div class="beer_photo">
					<img src="beer.png" alt="image"/>
				</div>
			{:else}
				<div class="beer_photo">
					<img src="{randomBeer.labels.medium}" alt="image" />
				</div>
			{/if}
			<div class="beer_name">
				<p>{randomBeer.name}</p>
			</div>
		</div></button></a>
		{/each}
	</div>

    <div class="gallery">
		{#each randomBreweries as randomBrewery}
		<a href="/brewery_info"><button class="btn_none" on:click={() => $id = randomBrewery.id}>
		<div class="beer_gallery">
			{#if randomBrewery.labels == undefined}
				<div class="beer_photo">
					<img src="beer.png" alt="image"/>
				</div>
			{:else}
				<div class="beer_photo">
					<img src="{randomBrewery.labels.medium}" alt="image" />
				</div>
			{/if}
			<div class="beer_name">
				<p>{randomBrewery.name}</p>
			</div>
		</div></button></a>
		{/each}
	</div>
    {/await}
</main>

<style>
    .gallery {
        overflow-x: scroll;
        overflow-y: hidden;
        white-space: nowrap;
        margin-bottom: 20px;
        width: 100%;
        overflow: auto;
    } 
    .gallery .btn_none {
        display: inline-block;
    }

</style>