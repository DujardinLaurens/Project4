<script>
    import Header from '../slots/header.svelte';
    import Loader from '../slots/loader.svelte';
    import { params } from '@sveltech/routify';
    import axios from 'axios';
    import Map from '../slots/map.svelte';
    import MapMarker from '../slots/mapMarker.svelte';
    import { onMount } from 'svelte';

    let brewery = []; 
    let promise = getBreweryInfo();
    let comment = "";
    let locations = [];
    let specificLocations = [];

	async function getBreweryInfo(){
		const res = await fetch(`https://sandbox-api.brewerydb.com/v2/brewery/${$params.breweryId}?&key=395c2bade2ee114e421a9228d3cbc512`);
		const json = await res.json();
		brewery = json.data;
        console.log(brewery);
    };

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

    function addComment() {
      axios({
        method: 'post',
        url: "http://localhost:50698/api/comment",
        headers: {
            Authorization : 'Bearer ' + getCookie('token')
        },
        data: {
            user: getCookie("username"),
            selectedId: $params.breweryId,
            comment: comment
        }
      })
      .then(response => console.log(response))
      .catch(err => console.log(err))
    }

    async function test() {
        let breweryLocations = [];
        const res = await fetch(`https://sandbox-api.brewerydb.com/v2/locations/?&key=395c2bade2ee114e421a9228d3cbc512`);
		const json = await res.json();
        locations = json.data;
        console.log(locations);
        for(let i = 0; i < locations.length; i++){
            if(locations[i].brewery.id == $params.breweryId){
                breweryLocations.push(locations[i]);
            }
        }
        specificLocations = breweryLocations;
    }
    test();
    
    onMount(function showCommentFunction(){
        let cookie = getCookie('loggedIn');
        if(cookie){
            document.getElementById('comment_form').style.display = "";
        }
        else {
            document.getElementById('comment_form').style.display = "none";
        }
    })
</script>

<main>
    {#await promise}
        <Loader></Loader>
    {:then}
        <Header headerMessage="{brewery.name}" logoImage="../beer.png"></Header>
        {#if !brewery.images && !brewery.established && !brewery.description && !brewery.website}
            <p>No further info</p>
        {:else}
            {#if !brewery.images}
                <div></div>
            {:else}
                <div class="img_div">
                    <img src="{brewery.images.large}" alt="brewery image"/>
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
        <Map lat={38} lon={-50} zoom={2.5}>
            {#each specificLocations as spec}
                <MapMarker lat={spec.latitude} lon={spec.longitude} label={spec.name}/>
            {/each}
        </Map>
        <form id="comment_form" on:submit|preventDefault={addComment}>
            <div class="container">
                <label for="psw"><b>Comment</b></label>
                <textarea bind:value={comment} type="comment" placeholder="place a comment..." name="comment" required></textarea>

                <label>
                <button type="submit">Submit</button>
            </div>
        </form>
    {/await}
</main>

<style>
    .info {
        margin: 0 2%;
    }

    .img_div{
        display: flex;
        justify-content: center;
    }
</style>