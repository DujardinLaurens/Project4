<script>
    import Header from './slots/header.svelte'
</script>

<main>
    <Header></Header>
    <h2>Login</h2>

    <form action="/" method="post">

        <div class="container">
            <label for="uname"><b>Username</b></label>
            <input type="text" placeholder="Enter Username" name="uname" required>

            <label for="psw"><b>Password</b></label>
            <input type="password" placeholder="Enter Password" name="psw" required>
            
            <label>
            <button type="submit">Login</button>
        </div>
    </form>
</main>

<style>

</style>