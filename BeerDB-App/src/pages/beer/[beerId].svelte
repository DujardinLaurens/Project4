<script>
    import Header from '../slots/header.svelte';
    import Loader from '../slots/loader.svelte';
    import axios from 'axios';
    import { params } from '@sveltech/routify';
    import { onMount } from 'svelte';

    let beer = [];
    let promise = getBeerInfo();
    let comment = "";
    let image = "";
    let commentedBeers = [];
    let commentedPromise = getCommentedBeers();

	async function getBeerInfo(){
		const res = await fetch(`https://sandbox-api.brewerydb.com/v2/beer/${$params.beerId}?&key=395c2bade2ee114e421a9228d3cbc512`);
		const json = await res.json();
		beer = json.data;
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
            selectedId: $params.beerId,
            image: image,
            comment: comment
        }
      })
      .then(function(response){
          window.location.reload();
      })
      .catch(function(err) {
        if(err.response.status === 401) {
            axios({
                method: 'post',
                url: "http://localhost:50698/api/auth/logout",
            })
            .then(function(response){
                var cookies = document.cookie.split(";");
                for (var i = 0; i < cookies.length; i++) {
                    var cookie = cookies[i];
                    var eqPos = cookie.indexOf("=");
                    var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
                    document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
                }
                window.location='../login'
            })
            .catch(err => console.log(err))
        }
      })
    }

    async function getCommentedBeers(){
		const res = await fetch(`http://localhost:50698/api/comment/${$params.beerId}`);
		const json = await res.json();
        commentedBeers.push(json);
        console.log(commentedBeers[0]);
    };
    
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
        <Header headerMessage="{beer.name}" logoImage="../beer.png"></Header>
        <div class="info">
            {#if !beer.labels}
                <p></p>
            {:else}
            <div class="img_div">
                <img src="{beer.labels.medium}" alt="beer picture" />
            </div>
            {/if}
            {#if !beer.abv}
                <p></p>
            {:else}
                <p>ABV: {beer.abv}%</p>
            {/if}
            {#if !beer.isOrganic}
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
                <p>Delicious with: {beer.foodPairings}</p>
            {/if}
        </div>
        <form id="comment_form" on:submit|preventDefault={addComment}>
            <div class="comment_container">
                <label for="psw"><b>Comment</b></label>
                <textarea bind:value={comment} type="comment" placeholder="place a comment..." name="comment" required></textarea>
                <label>
                <button type="submit">Submit</button>
            </div>
        </form>
        <div class="comments">
            {#each commentedBeers[0] as comments}
                <div class="comments__comment">
                    <h3>{comments.user}</h3>
                    <p>{comments.comment}</p>
                    <p>{comments.timePosted}</p>
                </div>
            {/each}
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
    
    .img_div {
        display: flex;
        justify-content: center;
    }
</style>