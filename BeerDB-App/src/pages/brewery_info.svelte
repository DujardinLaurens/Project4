<script>
    import Header from './slots/header.svelte';
    import Loader from './slots/loader.svelte';
    import { id } from '../store.js';

    let brewery = [];
    
    let promise = getBreweryInfo();

	async function getBreweryInfo(){
		const res = await fetch(`https://sandbox-api.brewerydb.com/v2/brewery/${$id}?&key=395c2bade2ee114e421a9228d3cbc512`);
		const json = await res.json();
		brewery = json.data;
        console.log(brewery);
    };
</script>

<main>
    <Header></Header>
    {#await promise}
        <Loader></Loader>
    {:then}
        <h1 class="name">{brewery.name}</h1>
        {#if !brewery.images && !brewery.established && !brewery.description && !brewery.website}
            <p>No further info</p>
        {:else}
            {#if !brewery.images}
                <div></div>
            {:else}
                <div>
                    <img src="{brewery.images.medium}" alt="brewery image"/>
                </div>
            {/if}
            <div class="info">
                {#if !brewery.established}
                    <div></div>
                {:else}
                    <div>
                        <p>Established in: {brewery.established}</p>
                    </div>
                {/if}

                {#if !brewery.description}
                    <div></div>
                {:else}
                    <div>
                        <p>{brewery.description}</p>
                    </div>
                {/if}

                {#if !brewery.website}
                    <div></div>
                {:else}
                    <div>
                        Website : <a href="{brewery.website}">{brewery.website}</a>
                    </div>
                {/if}
            </div>
        {/if}
    {/await}
</main>

<style>
    .name {
        margin-left: 2%;
    }

    .info {
        margin: 0 2%;
    }
</style>