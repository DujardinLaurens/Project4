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
		console.log(beers)
	};

	function filterBeers() {
	let input, filter, table, tr, td, i, txtValue;
	input = document.getElementById("input");
	filter = input.value.toUpperCase();
	table = document.getElementById("gallery");
	tr = table.getElementsByTagName("a");
	for (i = 0; i < tr.length; i++) {
		td = tr[i].getElementsByTagName("button")[0];
		if (td) {
		txtValue = td.textContent || td.innerText;
		if (txtValue.toUpperCase().indexOf(filter) > -1) {
			tr[i].style.display = "";
		} else {
			tr[i].style.display = "none";
		}
		}       
	}
	}
</script>

<main>
	<Header headerMessage={"A list of all beers"} logoImage="beer.png"></Header>
	{#await promise}
	<Loader></Loader>
	{:then}
	<input type="text" id="input" on:keyup={filterBeers} placeholder="Search for beers.." title="Type in a beer">
	<div id="gallery" class="gallery">
		{#each beers as beer}
		<a id="listItem" href={$url('/beer/:beerId', {beerId: `${beer.id}`})}><button id="btn_none" class="btn_none">
		<div id="beer_gallery" class="beer_gallery">
			{#if beer.labels == undefined}
				<div class="beer_image">
					<img src="beer.png" alt="image"/>
				</div>
			{:else}
				<div class="beer_image">
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

	.beer_image img {
		width: 196px;
		height: 196px;
	}
	#input {
		width: 20%;
	}
	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}
</style>