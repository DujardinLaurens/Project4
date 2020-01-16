<script>
    import Header from '../slots/header.svelte';
    import Loader from '../slots/loader.svelte';
    import axios from 'axios';
    import { params } from '@sveltech/routify';

    let beer = [];
    
    let promise = getBeerInfo();

	async function getBeerInfo(){
		const res = await fetch(`https://sandbox-api.brewerydb.com/v2/beer/${$params.beerId}?&key=395c2bade2ee114e421a9228d3cbc512`);
		const json = await res.json();
		beer = json.data;
        console.log(beer);
    };

    let comment = "";

    function getCookie(cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for(var i = 0; i <ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
            c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
            }
        }
        return "";
    }

    console.log(getCookie("username"));
    console.log($params.beerId)

    function addComment() {
      axios({
        method: 'post',
        url: "http://localhost:50698/api/comment",
        data: {
            user: getCookie("username"),
            beerId: $params.beerId,
            comment: comment
        }
      })
      .then(response => console.log(response))
      .catch(err => console.log(err))
    }


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
                    <img class="glass_img" src='../{beer.glass.name}.jpg' alt="beer glass type"/>
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

    <form on:submit|preventDefault={addComment}>

        <div class="container">
            <label for="psw"><b>Comment</b></label>
            <textarea bind:value={comment} type="comment" placeholder="place a comment..." name="comment" required></textarea>
            
            <label>
            <button type="submit">Submit</button>
        </div>
    </form>
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