<script>
	import { onMount } from 'svelte';
	import Header from './slots/header.svelte';

	let beer = [];

	onMount(async () => {
		const res = await fetch(`https://sandbox-api.brewerydb.com/v2/beer/c4f2KE?&key=395c2bade2ee114e421a9228d3cbc512`);
		const json = await res.json();
		beer = json.data;
        console.log(beer);
	});
</script>

<main>
    <Header></Header>
    <!-- <img src="{beer.labels.icon}" alt="beer image"/> -->
    <h1>{beer.name}</h1>
    <p>ABV: {beer.abv}%</p>
    <div>
        <!-- <p>style: {beer.style.name}</p> -->
    </div>
</main>