<script>
    import Header from './slots/header.svelte'

    function checkIfPswCorrect(form){
        password1 = form.psw.value; 
        password2 = form.confirm_psw.value;
        if(password1 != password2){
            alert("passwords did not match: please try again...");
            return false;
        }
        else {
            return true;
        }
    }
</script>

<main>
    <Header></Header>
    <h2>Register</h2>

    <form action="/" method="post" onSubmit = "return checkIfPswCorrect(this)">

        <div class="container">
            <label for="uname"><b>Username</b></label>
            <input type="text" placeholder="Enter Username" name="uname" required>

            <label for="psw"><b>Password</b></label>
            <input type="password" placeholder="Enter Password" name="psw" required>

            <label for="psw"><b>Confirm password</b></label>
            <input type="password" placeholder="Confirm Password" name="confirm_psw" required>
            
            <label>
            <button type="submit">Register</button>
        </div>
    </form>
</main>

<style>

</style>