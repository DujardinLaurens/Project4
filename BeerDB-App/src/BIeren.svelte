<script>
	import { onMount } from 'svelte';

	let bieren = [];

	
	const pages = [
		...new Array(23).fill(1).map((x, i) => i)
	]
	
	async function gotoPage (page) {
		console.log(`go to page ${page}`)
		const res = await fetch(`https://sandbox-api.brewerydb.com/v2/beers?p=${page}&key=395c2bade2ee114e421a9228d3cbc512`);
		const json = await res.json();
		bieren = json.data;
	}

	onMount(async () => {
		const res = await fetch('https://sandbox-api.brewerydb.com/v2/beers?p=1&key=395c2bade2ee114e421a9228d3cbc512');
		const json = await res.json();
		bieren = json.data;
		console.log(bieren)
	});
</script>

<main>
	<div class="gallery">
		{#each bieren as bier}
			<p>{bier.name}</p>
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

	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}
</style>