<script>
    import Header from './slots/header.svelte';
	import Loader from './slots/loader.svelte';
	import { url } from '@sveltech/routify';

    let randomBeer = [];
    let randomBrewery = [];

    let promise = getRandomBeer();
    let prom = getRandomBrewery();

	async function getRandomBeer(){
        const res = await fetch(`https://sandbox-api.brewerydb.com/v2/beer/random?key=395c2bade2ee114e421a9228d3cbc512`);
		const json = await res.json();
        randomBeer = json.data;
        console.log(randomBeer);
    };
    
    async function getRandomBrewery(){
        const res = await fetch(`https://sandbox-api.brewerydb.com/v2/brewery/random?key=395c2bade2ee114e421a9228d3cbc512`);
		const json = await res.json();
        randomBrewery = json.data;
        console.log(randomBrewery);
	};
</script>

<main>
    <Header headerMessage={"Welcome to the ultimate beer website"} logoImage="beer.png"></Header>
	{#await promise}
	<Loader></Loader>
	{:then}
	<div class="home">
		<div class="gallery">
			<div class="gallery_text">
				<h2>Enjoy this random beer: </h2>
			</div>
			<div class="beer_gallery"><a href={$url('/beer/:beerId', {beerId: `${randomBeer.id}`})}><button class="btn_none">
				<div class="image">
					<img src="beer.png" alt="image"/>
				</div>
				<div class="beer_name">
					<p>{randomBeer.name}</p>
				</div>
			</button></a></div>
		</div>

		<div class="gallery">
			<div class="gallery_text">
				<h2>Check out this brewery: </h2>
			</div>
			<div class="brewery_gallery"><a href={$url('/brewery/:breweryId', {breweryId: `${randomBrewery.id}`})}><button class="btn_none">
			
				<div class="image">
					<img src="brewery.png" alt="image"/>
				</div>
				<div class="brewery_name">
					<p>{randomBrewery.name}</p>
				</div>
			</button></a></div>
		</div>
	</div>
    {/await}
</main>

<style>
    .home {
		display: flex;
		justify-content: center;
		align-items: center;
        width: 100%;
    }
	.home div {
		display: flex;
		flex-direction: column;
		justify-content: center;
		align-items: center;
		width: 100%;
	}
	.image img {
		width: 196px;
		height: 196px;
	}
	.btn_none {
		width: 100%;
		display: inline-grid;
		padding: 10px;
		margin: 20px;
		background: transparent;
		border: transparent;
		cursor: pointer;
	}
</style>