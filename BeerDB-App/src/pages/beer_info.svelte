<script>
    import Header from './slots/header.svelte';
    import Loader from './slots/loader.svelte';
    import { id } from '../store.js';

    let beer = [];
    
    let promise = getBeerInfo();

	async function getBeerInfo(){
		const res = await fetch(`https://sandbox-api.brewerydb.com/v2/beer/${$id}?&key=395c2bade2ee114e421a9228d3cbc512`);
		const json = await res.json();
		beer = json.data;
        console.log(beer);
    };
</script>

<main>
    <Header></Header>
    {#await promise}
        <Loader></Loader>
    {:then}
        <div class="info">
            <h1>{beer.name}</h1>

            {#if !beer.labels}
                <div></div>
            {:else}
                <div class="beer_photo">
                    <img src="{beer.labels.medium}" alt="bier foto" />
                </div>
            {/if}

            {#if ! beer.abv}
                <p></p>
            {:else}
                <p>ABV: {beer.abv}%</p>
            {/if}

            {#if ! beer.isOrganic}
                <p></p>
            {:else}
                <p>Organic?: {beer.isOrganic}</p>
            {/if}

            {#if !beer.style}
                <p></p>
            {:else}
                <p>Category: {beer.style.category.name}</p>
                <p>{beer.style.description}</p>
            {/if}

            {#if !beer.glass}
                <p></p>
            {:else}
                <div class="beer_glass">
                    <p>Beer glass type: </p>
                    <img class="glass_img" src='{beer.glass.name}.jpg' alt="beer glass type"/>
                </div>
            {/if}

            {#if !beer.foodPairings}
                <p></p>
            {:else}
                <div>
                    <p>Delicious with: {beer.foodPairings}</p>
                </div>
            {/if}
        </div>
    {/await}
</main>

<style>
    .info {
        margin: 0 2%;
    }
    
    .beer_glass{
        display: flex;
        align-items: center;
    }

    .glass_img {
        width: 100px;
        height: 100px;
    }
</style>